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

        public int ItemID { get; set; }

        [ForeignKey("ItemID")]
        public virtual InventoryItem Item { get; set; }

        public int PurchaseOrderID { get; set; }

        [ForeignKey("PurchaseOrderID")]
        public PurchaseOrder PurchaseOrder { get; set; }


        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please order 1 or more items.")]
        public int QuantityOrdered { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid quantity.")]
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
        public string ItemName { get; set; }
    }
}
