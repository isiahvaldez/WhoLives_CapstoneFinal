using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhoLives.Models.ViewModels
{
    public class ItemAssembleVM
    {
        public Vendor Vendor { get; set; }
        public List<int> ItemS { get; set; }
    }
}
