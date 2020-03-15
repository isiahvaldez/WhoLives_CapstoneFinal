using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhoLives.Models
{
    public class PurchaseOrders
    {
        [Key]
        public int PurchaseOrderID { get; set; }

        [ForeignKey("Vendor")]
        public int VendorID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOrdered { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateDelivered { get; set; }

        public decimal TotalOrderPrice { get; set; }

        public string VendorPO { get; set; }

        public string LastModifiedBy { get; set; }

        public bool Received { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime LastModifiedDate { get; set; }

        //public List<OrderItem> OrderItems { get; set; }

        //public Vendor Vendor { get; set; }
    }
}
