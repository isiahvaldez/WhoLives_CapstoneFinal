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

        //[HttpGet("{id}")]
        public IActionResult OnGet(int? id)
        {
            InventoryItemVM = new InventoryItemVM
            {
                OrderInfo = new PurchaseOrder(),
                ItemList = _unitOfWork.InventoryItems.GetItemListForDropDown(),
                MeasureInfo = _unitOfWork.Measures.GetMeasureListForDropDown(),
                BuildInfo = new BuildAssembly(),
                AssemblyInfo = new Assembly(),
                InventoryItemObj = new InventoryItem()
            };

            if (id != null)
            {
                InventoryItemVM.InventoryItemObj = _unitOfWork.InventoryItems.GetFirstOrDefault(u => u.InventoryItemID == id);
                if (InventoryItemVM.InventoryItemObj == null)
                {
                    return NotFound();
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
            //return RedirectToPage(new { id=InventoryItemVM.InventoryItemObj.InventoryItemID, isEditable = true});
        }
        /*
        [HttpGet("{id,isEditable}")]
        public IActionResult OnGet(int? id, bool isEditable)
        {
            InventoryItemVM = new InventoryItemVM
            {
                OrderInfo = new PurchaseOrder(),
                ItemList = _unitOfWork.InventoryItems.GetItemListForDropDown(),
                MeasureInfo = _unitOfWork.Measures.GetMeasureListForDropDown(),
                BuildInfo = new BuildAssembly(),
                AssemblyInfo = new Assembly(),
                InventoryItemObj = new InventoryItem()
            };

            isEditable = this.isEditable;

            if (id != null)
            {
                InventoryItemVM.InventoryItemObj = _unitOfWork.InventoryItems.GetFirstOrDefault(u => u.InventoryItemID == id);
                if (InventoryItemVM.InventoryItemObj == null)
                {
                    return NotFound();
                }
            }
            return Page();

        }*/
    }
}
