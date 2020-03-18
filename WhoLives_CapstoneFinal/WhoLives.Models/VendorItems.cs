using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhoLives.Models
{
    public class VendorItems
    {

        public int InventoryItemsId { get; set; }
        public int VendorId { get; set; }

       
        public InventoryItems InventoryItems { get; set; }
     
        public Vendor Vendor { get; set; }
    }
}
