using System;
using System.Collections.Generic;
using System.Text;

namespace WhoLives.Models.ViewModels
{
    public class PurchaseOrderVM
    {
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public PurchaseOrder OrderInfo { get; set; }
    }
}
