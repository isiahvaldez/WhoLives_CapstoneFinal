using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;
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

        [BindProperty]
        public BuildAssemblyVM BuildAssemblyVM { get; set; }

        //public IEnumerable<Assembly> AssemblyList { get; set; }
        public IActionResult OnGet(int? id)
        {
            InventoryItemVM = new InventoryItemVM
            {
                PurchaseOrderInfo = _unitOfWork.PurchaseOrders.GetAll().ToList(),
                ItemList = _unitOfWork.InventoryItems.GetItemListForDropDown(),
                MeasureInfo = _unitOfWork.Measures.GetMeasureListForDropDown(),
                BuildInfo = new BuildAssembly(),
                AssemblyInfo = new Assembly(),
                InventoryItemObj = new InventoryItem()
            };

            BuildAssemblyVM = new BuildAssemblyVM
            {
                InventoryItems = _unitOfWork.InventoryItems.GetAll(),
                BuildAssemblies = _unitOfWork.BuildAssemblies.GetAll().Where(i => i.InventoryItemID == id),
                InventoryItem = _unitOfWork.InventoryItems.GetFirstOrDefault(i => i.InventoryItemID == id),
                Assemblies = _unitOfWork.Assemblies.GetAll()
            };

            //BuildAssemblyVM.Assemblies.App

            if (id != null)
            {
                InventoryItemVM.InventoryItemObj = _unitOfWork.InventoryItems.GetFirstOrDefault(u => u.InventoryItemID == id);
                if (InventoryItemVM.InventoryItemObj == null)
                {
                    return NotFound();
                }
                InventoryItemVM.InventoryItemObj.BuildAssemblyList = _unitOfWork.BuildAssemblies.GetAll().Where(b => b.InventoryItemID == id).ToList();
                
                
            }
            return Page();
        }
    }
}