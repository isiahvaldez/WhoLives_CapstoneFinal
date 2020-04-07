using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;
using WhoLives.Models.ViewModels;
using static WhoLives_CapstoneFinal.Controllers.OrderController;

namespace WhoLives_CapstoneFinal.Pages.PurchaseOrders
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _uow;
        public UpsertModel(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [BindProperty]
        public PurchaseOrderVM PurchaseOrderVM { get; set; }
        public IActionResult OnGet(int? id)
        {
            PurchaseOrderVM = new PurchaseOrderVM
            {
                OrderInfo = new PurchaseOrder(),
                ItemList = _uow.InventoryItems.GetItemListForDropDown(),
                VendorList = _uow.Vendors.GetVendorListForDropDown(),
                StatusList = _uow.Statuses.GetStatusListForDropDown(),
                tempOrderItem = new OrderItem()
            };
            if (id != null)
            {
                PurchaseOrderVM.OrderInfo = _uow.PurchaseOrders.GetFirstOrDefault(u => u.PurchaseOrderID == id);
                if (PurchaseOrderVM == null)
                {
                    return NotFound();
                }
                PurchaseOrderVM.OrderInfo.OrderItems = _uow.OrderItems.GetAll(o => o.PurchaseOrderID == id, null, null).ToList();
                foreach(var l in PurchaseOrderVM.OrderInfo.OrderItems)
                {
                    l.Item = _uow.InventoryItems.GetFirstOrDefault(i => i.InventoryItemID == l.ItemID);
                }
            }
            return Page(); //No params refreshes the page
        }
        public ActionResult OnPostSaveOrder(List<OrderItem> orderItems)
        {
            // if problems, bail
            if (orderItems == null)
            {
                // setup new list
                orderItems = new List<OrderItem>();
            }

            // loop list from ajax
            foreach (OrderItem item in orderItems)
            {
                // set purchase order id
                item.PurchaseOrderID = PurchaseOrderVM.OrderInfo.PurchaseOrderID;
                // add item to db
                //_context.OrderItems.Add(item);
            }
            // save db
            //_context.SaveChanges();
            // return results back to ajax call
            return new JsonResult("done");
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                PurchaseOrderVM.VendorList = _uow.Vendors.GetVendorListForDropDown();
                return Page();
            }
            if(PurchaseOrderVM.OrderInfo.PurchaseOrderID == 0)
            {
                _uow.PurchaseOrders.Add(PurchaseOrderVM.OrderInfo);
            }
            else
            {
                _uow.PurchaseOrders.update(PurchaseOrderVM.OrderInfo);
            }
            _uow.Save();
            if (PurchaseOrderVM.OrderInfo.PurchaseOrderID != 0)
            {
                //Get the current order items in the db to compare
                var DBItems = _uow.OrderItems.GetAll(o => o.PurchaseOrderID == PurchaseOrderVM.OrderInfo.PurchaseOrderID).ToList();

                if (PurchaseOrderVM.OrderInfo.OrderItems.Count > 0)
                {
                    foreach (var o in PurchaseOrderVM.OrderInfo.OrderItems)
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
            return RedirectToPage("./Index");
        }
        public void ExportCSV()
        {
            //var list = _uow.OrderItems.ExportList(PurchaseOrderVM.OrderInfo.OrderItems);
            //return list;
        }
        //public IActionResult OnPostAddOrderItem()
        //{
        //    _uow.OrderItems.Add(new OrderItem
        //    {
        //        ItemID = PurchaseOrderVM.tempOrderItem.ItemID,
        //        PurchaseOrderID = PurchaseOrderVM.OrderInfo.PurchaseOrderID,
        //        Price = PurchaseOrderVM.tempOrderItem.Price,
        //        QuantityOrdered = PurchaseOrderVM.tempOrderItem.QuantityOrdered,
        //        QuantityReceived = PurchaseOrderVM.tempOrderItem.QuantityReceived
        //    });
        //    return Page();
        //}
    }
}