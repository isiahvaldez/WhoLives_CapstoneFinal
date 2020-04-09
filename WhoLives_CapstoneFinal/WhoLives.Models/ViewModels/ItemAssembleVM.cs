using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhoLives.Models.ViewModels
{
    public class ItemAssembleVM
    {
        public IEnumerable<SelectListItem> VendorList { get; set; }
        public Vendor Vendor { get; set; }
        public IEnumerable<SelectListItem> VendorItemList { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }
    }
}
