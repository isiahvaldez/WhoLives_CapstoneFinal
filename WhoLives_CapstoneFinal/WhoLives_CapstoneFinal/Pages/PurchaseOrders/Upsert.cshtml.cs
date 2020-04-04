﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;
using WhoLives.Models.ViewModels;

namespace WhoLives_CapstoneFinal
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _uow;
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public UpsertModel(IUnitOfWork uow)
        {
            _uow = uow;
            StatusList = new List<SelectListItem>()
            {
                new SelectListItem{Text = "Back Order", Value = "backorder"},
                new SelectListItem{Text = "Ordered", Value = "ordered"},
                new SelectListItem{Text = "Shipping", Value = "shipping"},
                new SelectListItem{Text = "Received", Value = "received"},
                new SelectListItem{Text = "Partially Received", Value = "partially"},
                new SelectListItem{Text = "Pending", Value = "pending"},
                new SelectListItem{Text = "Overdue", Value = "overdue"},
            };
        }
        [BindProperty]
        public PurchaseOrderVM PurchaseOrderVM { get; set; }
        public IActionResult OnGet(int? id)
        {
            PurchaseOrderVM = new PurchaseOrderVM
            {
                OrderInfo = new PurchaseOrder(),
                ItemList = _uow.InventoryItems.GetItemListForDropDown(),
                VendorList = _uow.Vendors.GetVendorListForDropDown()
            };
            if (id != null)
            {
                PurchaseOrderVM.OrderInfo = _uow.PurchaseOrders.GetFirstOrDefault(u => u.PurchaseOrderID == id);
                if (PurchaseOrderVM == null)
                {
                    return NotFound();
                }
                PurchaseOrderVM.OrderInfo.OrderItems = _uow.OrderItems.GetAll(o => o.PurchaseOrderID == id, null, null);
                foreach(var l in PurchaseOrderVM.OrderInfo.OrderItems)
                {
                    l.Item = _uow.InventoryItems.GetFirstOrDefault(i => i.InventoryItemID == l.ItemID);
                }
            }
            return Page(); //No params refreshes the page
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
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
            var POId = PurchaseOrderVM.OrderInfo.PurchaseOrderID;
            if (PurchaseOrderVM.OrderInfo.PurchaseOrderID != 0)
            {
                //Get the current order items in the db to compare
                var DBItems = _uow.OrderItems.GetAll(o => o.PurchaseOrderID == PurchaseOrderVM.OrderInfo.PurchaseOrderID).ToList();

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
                foreach (var i in DBItems)
                {
                    // Delete i from DB
                    _uow.OrderItems.Remove(i.OrderItemID);
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
    }
}