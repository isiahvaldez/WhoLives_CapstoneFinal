using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhoLives.Models.ViewModels
{
    public class ItemAssembleVM
    {
        public int Quantity { get; set; }
        public int ItemId { get; set; }
    }
}
