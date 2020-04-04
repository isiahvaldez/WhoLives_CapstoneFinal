using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhoLives.Models.ViewModels
{
    public class PurchaseOrderVM
    {
        public IEnumerable<SelectListItem> ItemList { get; set; }
        public IEnumerable<SelectListItem> VendorList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public PurchaseOrder OrderInfo { get; set; }
    }
}
