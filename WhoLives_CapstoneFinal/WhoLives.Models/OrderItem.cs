using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhoLives.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        [ForeignKey("Item")]
        public int ItemID { get; set; }

        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderID { get; set; }

        [ForeignKey("Vendor")]
        public int VendorID { get; set; }


        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }

        public bool ItemReceived { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateDelivered { get; set; }

        public string LastModifiedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime LastModifiedDate { get; set; }

        [NotMapped]
        public InventoryItem Item { get; set; }

        [NotMapped]
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}
