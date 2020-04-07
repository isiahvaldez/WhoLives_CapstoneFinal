using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;
using WhoLives.Models.ViewModels;

namespace WhoLives_CapstoneFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _uow;
        public OrderController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [HttpGet]
        public IActionResult Get(int? id)
        {
            if (id.HasValue)
            {
                return Json(new { data = _uow.OrderItems.GetAll(d => d.PurchaseOrderID == id, null, "Item") });
            }
            return Json(new { data = _uow.PurchaseOrders.GetAll(null, null, "Vendor,Status") });
        }

        [HttpGet("save")]
        public IActionResult Save(int id, int itemId, int qtyOrdered, string price, int qtyReceived)
        {
            _uow.OrderItems.Add(new OrderItem
            {
                PurchaseOrderID = id,
                ItemID = itemId,
                QuantityOrdered = qtyOrdered,
                Price = Convert.ToDecimal(price),
                QuantityReceived = qtyReceived,
                ItemReceived = false,
                DateDelivered = DateTime.Now,
                LastModifiedBy = "Order Controller",
                LastModifiedDate = DateTime.Now

            });
            _uow.Save();
            return Json(new { data = _uow.OrderItems.GetAll(d => d.PurchaseOrderID == id, null, "Item") });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _uow.PurchaseOrders.GetFirstOrDefault(p => p.PurchaseOrderID == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _uow.PurchaseOrders.Remove(objFromDb);
            _uow.Save();
            return Json(new { success = true, message = "Delete successful" });

        }

        public class myOrderSelection
        {
            public string Vendor { get; set; }
            public string[] Items { get; set; }
        }
        [HttpPost]
        public int FromReOrder([FromBody]myOrderSelection Selection)      
        {
            string TestId = Selection.Items[0];

            var PurchaseOrderVM = new PurchaseOrderVM
            {
                OrderInfo = new PurchaseOrder()
                {
                    StatusID = 6,
                    DateOrdered = DateTime.Now,
                    StatusChangeDate = DateTime.Now,
                    OrderItems = new List<OrderItem>(),
                    VendorID = Convert.ToInt32(Selection.Vendor)
                },
                ItemList = _uow.InventoryItems.GetItemListForDropDown(),
                VendorList = _uow.Vendors.GetVendorListForDropDown(),
                StatusList = _uow.Statuses.GetStatusListForDropDown()
            };
            _uow.PurchaseOrders.Add(PurchaseOrderVM.OrderInfo);
            _uow.Save();
            foreach (var i in Selection.Items)
            {
                var tempOrderItem = new OrderItem()
                {
                    ItemID = Convert.ToInt32(i),
                    QuantityReceived = 0,
                    ItemReceived = false,
                    Price = 0,
                    DateDelivered = DateTime.Now,
                    QuantityOrdered = 0,
                    LastModifiedBy = "Order Controller",
                    LastModifiedDate = DateTime.Now,
                    PurchaseOrderID = PurchaseOrderVM.OrderInfo.PurchaseOrderID
                };
                _uow.OrderItems.Add(tempOrderItem);
                PurchaseOrderVM.OrderInfo.OrderItems.Add(tempOrderItem);
            }
            _uow.Save();
            return PurchaseOrderVM.OrderInfo.PurchaseOrderID;





            //Take items (and vendor soon?) to the upsert page on a NEW purchase order
            //var page = new WhoLives_CapstoneFinal.Pages.PurchaseOrders.UpsertModel(_uow);
            //return page.Page();
            //return LocalRedirect("./Pages/PurchaseOrders/Upsert");
            //return RedirectToPage("./PurchaseOrders/Upsert", "reorder", new { vendor = Selection.Vendor, items = Selection.Items });
            //return RedirectToAction("reorder", "/PurchaseOrders/Upsert");
            //return RedirectToAction("../PurchaseOrders/Index");
            //return Ok();
        }

        private IActionResult Page()
        {
            throw new NotImplementedException();
        }
    }
}