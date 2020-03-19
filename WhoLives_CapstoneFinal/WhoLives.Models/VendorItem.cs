using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhoLives.Models
{
    public class VendorItem
    {

        public int InventoryItemID { get; set; }
        public int VendorID { get; set; }

       
        public InventoryItem InventoryItems { get; set; }
     
        public Vendor Vendor { get; set; }
    }
}
