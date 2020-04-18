using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WhoLives.DataAccess;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;
using WhoLives.Models.ViewModels;

namespace WhoLives_CapstoneFinal.Pages.Inventory
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public bool isEditable = false;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public InventoryItemVM InventoryItemVM { get; set; }

        public IActionResult OnGet(int? id)
        {
            InventoryItemVM = new InventoryItemVM
            {
                PurchaseOrderInfo = null,
                ItemList = _unitOfWork.InventoryItems.GetItemListForDropDown(),
                MeasureInfo = _unitOfWork.Measures.GetMeasureListForDropDown(),
                OrderItemInfo = null,
                BuildInfo = null,
                AssemblyInfo = null,
                InventoryItemObj = new InventoryItem()
            };

            if (id != null)
            {
                InventoryItemVM.InventoryItemObj = _unitOfWork.InventoryItems.GetFirstOrDefault(u => u.InventoryItemID == id);
                if (InventoryItemVM.InventoryItemObj == null)
                {
                    return NotFound();
                }
                else
                {
                    InventoryItemVM.OrderItemInfo = _unitOfWork.OrderItems.GetAll(o => o.ItemID == id).ToList();
                    var filter = new List<int>();
                    var temp = new List<OrderItem>();
                    foreach (var item in InventoryItemVM.OrderItemInfo)
                    {
                        filter.Add(item.PurchaseOrderID);
                    }

                    InventoryItemVM.PurchaseOrderInfo = _unitOfWork.PurchaseOrders.GetAll(x => filter.Contains(x.PurchaseOrderID), null, "Vendor").ToList();
                    for (int i =0; i< InventoryItemVM.PurchaseOrderInfo.Count; i++)
                    {foreach(var order in InventoryItemVM.OrderItemInfo.Where(x=>x.PurchaseOrderID == InventoryItemVM.PurchaseOrderInfo[i].PurchaseOrderID)){
                            temp.Add(order);
                        }
                        InventoryItemVM.PurchaseOrderInfo[i].OrderItems = temp;
                        temp = new List<OrderItem>();
                    }

                    InventoryItemVM.BuildListInfo = _unitOfWork.BuildAssemblies.GetAll(b => b.InventoryItemID == id,null,"Assembly").ToList();
                    filter.Clear();
                    foreach (var item in InventoryItemVM.BuildListInfo)
                    {
                        filter.Add(item.AssemblyID);
                    }
                    InventoryItemVM.AssemblyListInfo = _unitOfWork.Assemblies.GetAll(x => filter.Contains(x.AssemblyID), null, "InventoryItem").ToList();

                }
            }
            return Page();

        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (InventoryItemVM.InventoryItemObj.InventoryItemID == 0) // create new inventory item
            {
                _unitOfWork.InventoryItems.Add(InventoryItemVM.InventoryItemObj);
            }
            else // edit inventory item
            {
                _unitOfWork.InventoryItems.Update(InventoryItemVM.InventoryItemObj);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
