using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives_CapstoneFinal.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<InventoryItem> InventoryItemList;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public WhoLives.Models.Vendor Vendor { get; set; }
        public IEnumerable<SelectListItem> VendorObjList{ get; set; }       
        public void OnGet()
        {
            VendorObjList = _unitOfWork.Vendors.GetVendorListForDropDown();

        }
    }
}