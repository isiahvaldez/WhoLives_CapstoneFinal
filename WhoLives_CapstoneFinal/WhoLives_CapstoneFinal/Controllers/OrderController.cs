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
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _uow;
        public OrderController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [HttpGet]
        [ActionName("")]
        public IActionResult Get(int? id)
        {
            if (id.HasValue)
            {
                return Json(new { data = _uow.OrderItems.GetAll(d => d.PurchaseOrderID == id, null, "Item") });
            }
            return Json(new { data = _uow.PurchaseOrders.GetAll(null, null, "Vendor,Status") });
        }

        [HttpGet("save")]
        [ActionName("")]
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
        [ActionName("")]
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
        [ActionName("")]
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


        [HttpPost("{id}")]
        [ActionName("id")]
        public IActionResult PostID(int id, [FromBody] PurchaseOrder purchaseOrderDetails)
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

        [HttpPost("{purchaseOrder}")]
        [ActionName("list")]
        public IActionResult PostList([FromBody] string componentList, string purchaseOrder)
        {
            //var purchaseOrder = "{ purchaseOrderID = 12 }";
            var orderItems = JsonConvert.DeserializeObject<IEnumerable<OrderItem>>(componentList);
            PurchaseOrder purchaseOrderDetails = JsonConvert.DeserializeObject<PurchaseOrder>(purchaseOrder);
            //Normal Post
            //if (!ModelState.IsValid)
            //{
            //    PurchaseOrderVM.VendorList = _uow.Vendors.GetVendorListForDropDown();
            //    return Page();
            //}
            if (purchaseOrderDetails.PurchaseOrderID == 0)
            {
                _uow.PurchaseOrders.Add(purchaseOrderDetails);
            }
            else
            {
                _uow.PurchaseOrders.update(purchaseOrderDetails);
            }
            _uow.Save();
            //Now update the order items
            if (purchaseOrderDetails.PurchaseOrderID != 0)
            {
                //Get the current order items in the db to compare
                var DBItems = _uow.OrderItems.GetAll(o => o.PurchaseOrderID == purchaseOrderDetails.PurchaseOrderID).ToList();

                if (orderItems.Count() > 0)
                {
                    foreach (var o in orderItems)
                    {
                        if (DBItems.Contains(o))
                        {
                            // Update o in DB
                            _uow.OrderItems.update(o);
                            // Remove o from DBItems list
                            DBItems.Remove(o);
                        }
                        else
                        {
                            // Add o to DB
                            _uow.OrderItems.Add(o);
                        }
                    }
                }
                if (DBItems.Count > 0)
                {
                    foreach (var i in DBItems)
                    {
                        // Delete i from DB
                        _uow.OrderItems.Remove(i.OrderItemID);
                    }
                }
                _uow.Save();
            }
            return Json(new { msg = "success" });
        }
    }
}