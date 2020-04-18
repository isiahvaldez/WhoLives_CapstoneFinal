using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;
using WhoLives.Models.ViewModels;

namespace WhoLives_CapstoneFinal.Pages.Inventory
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<InventoryItem> InventoryItemList;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public ItemAssembleVM ItemAssemblyVendor { get; set; }
        public void OnGet()
        {
            ItemAssemblyVendor = new ItemAssembleVM()
            {
                VendorList = _unitOfWork.Vendors.GetVendorListForDropDown(),
                VendorItemList = _unitOfWork.VendorItems.GetVendorListForDropDown(),
                Items = _unitOfWork.InventoryItems.GetItemListForDropDown()

            };

        }
    }
}