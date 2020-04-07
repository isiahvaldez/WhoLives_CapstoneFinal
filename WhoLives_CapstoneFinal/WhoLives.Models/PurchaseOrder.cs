using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhoLives.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int PurchaseOrderID { get; set; }

        [Display(Name = "Vendor")]
        public int VendorID { get; set; }

        [ForeignKey("VendorID")]
        public Vendor Vendor { get; set; }

        [Required]
        [Display(Name = "Date Ordered")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOrdered { get; set; }

        [Required]
        [Display(Name = "Status Updated")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StatusChangeDate { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Purchase Order #")]
        public string PO { get; set; }

        [Display(Name = "Status")]
        public int StatusID { get; set; }

        [ForeignKey("StatusID")]
        public Status Status { get; set; }

        public string LastModifiedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime LastModifiedDate { get; set; }

        [NotMapped]
        public List<OrderItem> OrderItems { get; set; }
    }
}
