using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;
using WhoLives.Models.ViewModels;

namespace WhoLives_CapstoneFinal
{
    public class AssemblyModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssemblyModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public InventoryItemVM InventoryItemVM { get; set; }
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
    }
}