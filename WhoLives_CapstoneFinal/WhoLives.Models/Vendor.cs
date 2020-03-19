using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WhoLives.Models
{
    public class Vendor
    {
        [Key]
        public int VendorID { get; set; }
        [Display(Name = "Name")]
        public string VendorName { get; set; }
        [Display(Name = "Website")]
        public string VendorWebsite { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string LastModifiedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime LastModifiedDate { get; set; }

        public ICollection<VendorItems>VendorItems { get; set; } // This is needed Per Contoso University Tutorial - JDW

        //public ICollection<PurchaseOrder> PurchaseOrders { get; set; } // probably belongs in a View Model
    }
}
