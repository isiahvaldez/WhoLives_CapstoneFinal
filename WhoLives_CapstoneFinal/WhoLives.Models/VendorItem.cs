using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhoLives.Models
{
    public class VendorItem
    {
        [Key]
        public int VendorItemId { get; set; }

        public int InventoryItemID { get; set; }
        public int VendorID { get; set; }

        [ForeignKey("InventoryItemID")]
        public InventoryItem InventoryItem { get; set; }

        [ForeignKey("VendorID")]
        public Vendor Vendor { get; set; }
    }
}
