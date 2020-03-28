using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhoLives.DataAccess;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives_CapstoneFinal.Pages.Inventory
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public InventoryItem InventoryItemObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            //MenuItemObj = new MenuItemVM
            //{
            //    CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
            //    FoodTypeList = _unitOfWork.FoodType.GetFoodTypeListForDropDown(),
            //    MenuItem = new Models.MenuItem()
            //};

            InventoryItemObj = new WhoLives.Models.InventoryItem()


            if (id != null) //edit
            {
                MenuItemObj.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);
                if (MenuItemObj == null)
                {
                    return NotFound();
                }

                InventoryItemObj.InventoryItemID = _unitOfWork.InventoryItems.GetFirstOrDefault(u => u.InventoryItemID = id);
            }

            return Page();

        }

        public IActionResult OnPost()
        {
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
