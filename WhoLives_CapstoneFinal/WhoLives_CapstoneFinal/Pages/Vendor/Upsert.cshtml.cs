﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives_CapstoneFinal.Pages.Vendor
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public WhoLives.Models.Vendor VendorObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            VendorObj = new WhoLives.Models.Vendor();
            if (id != null) // edit
            {
                VendorObj = _unitOfWork.Vendor.GetFirstOrDefault(v => v.VendorID == id);
                if (VendorObj == null)
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

            if (VendorObj.VendorID == 0) // new food type 
            {
                _unitOfWork.Vendor.Add(VendorObj);
            }

            else // edit vendor
            {
                _unitOfWork.Vendor.Update(VendorObj);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}