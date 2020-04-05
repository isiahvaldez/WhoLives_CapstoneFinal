using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhoLives.Models.ViewModels
{
    public class InventoryItemVM
    {
        public IEnumerable<SelectListItem> ItemList { get; set; }
        public PurchaseOrder OrderInfo { get; set; }
        public Assembly AssemblyInfo { get; set; }
        public BuildAssembly BuildInfo { get; set; }
        public OrderItem OrderItemInfo { get; set; }
        public IEnumerable<SelectListItem> MeasureInfo { get; set; }
        public InventoryItem InventoryItemObj { get; set; }
    }
}
