using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;
using WhoLives.Models.ViewModels;

namespace WhoLives_CapstoneFinal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderItemController : Controller
    {
        private readonly IUnitOfWork _uow;
        public OrderItemController(IUnitOfWork uow)
        {
            _uow = uow;
        }
               
        [HttpPost("{id}")]
        [ActionName("id")]
        public IActionResult ID(int id, PurchaseOrder purchaseOrderDetails)
        {
            // delete all order items for the current purchase order id if any exist
            var ItemList = _uow.OrderItems.GetAll().Where(a => a.PurchaseOrderID == id);
            foreach (var orderItem in ItemList)
            {
                _uow.OrderItems.Remove(orderItem);
            }
            //Update the purchase order as necessary
            _uow.Save();

            return Json(new { msg = "success" });
        }
        public class ComponentObject
        {
            public OrderItem[] orderItems { get; set; }
            public PurchaseOrder purchaseOrderDetails { get; set; }
        }
        [HttpPost()]
        [ActionName("list")]
        public IActionResult List(ComponentObject component)
        {
            foreach(var o in component.orderItems)
            {
                o.ItemID = _uow.InventoryItems.GetFirstOrDefault(i => i.Name == o.ItemName).InventoryItemID;
            }
            if (component.purchaseOrderDetails.PurchaseOrderID == 0)
            {
                _uow.PurchaseOrders.Add(component.purchaseOrderDetails);
            }
            else
            {
                _uow.PurchaseOrders.update(component.purchaseOrderDetails);
            }
            _uow.Save();
            //Now update the order items
            if (component.purchaseOrderDetails.PurchaseOrderID != 0)
            {
                //Get the current order items in the db to compare
                var DBItems = _uow.OrderItems.GetAll(o => o.PurchaseOrderID == component.purchaseOrderDetails.PurchaseOrderID).ToList();

                if (component.orderItems.Count() > 0)
                {
                    decimal newTotal = 0.00M;
                    foreach (var o in component.orderItems)
                    {
                        if(component.purchaseOrderDetails.StatusID == _uow.Statuses.GetFirstOrDefault(s => s.Name == "Received").StatusId)
                        {
                            o.QuantityReceived = o.QuantityOrdered;
                        }
                        newTotal += (o.Price * o.QuantityOrdered);
                        if (DBItems.Where(d => d.OrderItemID == o.OrderItemID).Count() > 0)
                        {
                            // Update o in DB
                            _uow.OrderItems.update(o);
                            // Remove o from DBItems list
                            DBItems.Remove(DBItems.Where(d => d.OrderItemID == o.OrderItemID).FirstOrDefault());
                        }
                        else
                        {
                            // Add o to DB
                            _uow.OrderItems.Add(o);
                        }
                        // Update vendorItem table
                        if (_uow.VendorItems.GetAll(v =>
                             v.VendorID == component.purchaseOrderDetails.VendorID &&
                             v.InventoryItemID == o.ItemID).ToList().Count() == 0)
                        {
                            _uow.VendorItems.Add(new VendorItem
                            {
                                VendorID = component.purchaseOrderDetails.VendorID,
                                InventoryItemID = o.ItemID
                            });
                        }
                    }
                    component.purchaseOrderDetails.TotalPrice = newTotal;
                    _uow.PurchaseOrders.update(component.purchaseOrderDetails);
                }
                if (DBItems.Count > 0)
                {
                    foreach (var i in DBItems)
                    {
                        // Delete i from DB
                        _uow.OrderItems.Remove(i.OrderItemID);
                        // Update vendorItems table
                    }
                }
                _uow.Save();
            }
            return Json(new { msg = "success" });
        }
    }
}