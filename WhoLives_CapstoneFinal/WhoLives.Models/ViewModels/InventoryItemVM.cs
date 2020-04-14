using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhoLives.Models.ViewModels
{
    public class InventoryItemVM
    {
        public IEnumerable<SelectListItem> ItemList { get; set; }
        public List<PurchaseOrder> PurchaseOrderInfo { get; set; }
        public Assembly AssemblyInfo { get; set; }
        public BuildAssembly BuildInfo { get; set; }
        public List<Assembly> AssemblyListInfo { get; set; }
        public List<BuildAssembly> BuildListInfo { get; set; }
        public List<OrderItem> OrderItemInfo { get; set; }
        public IEnumerable<SelectListItem> MeasureInfo { get; set; }
        public InventoryItem InventoryItemObj { get; set; }
    }
}
