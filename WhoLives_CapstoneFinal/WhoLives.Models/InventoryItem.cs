﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhoLives.Models
{
    public class InventoryItem
    {
        //Primary key for the Item table
        [Key]
        public int InventoryItemID { get; set; }

        //Identifier if the part is an assembly
        public bool IsAssembly { get; set; }

        //Part name
        [Required]
        [StringLength(255)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        //Part details
        [Display(Name = "Description")]
        public string Description { get; set; }

        //Number of loose items available that are not used in an assembly
        [Display(Name = "Loose Quantity")]
        public int TotalLooseQty { get; set; }

        //Cost to WhoLives to purchase the item
        [Display(Name = "Retail Cost")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal ListRetailCost { get; set; }

        //Minimum number of parts to have available
        [Display(Name = "Reorder Quantity")]
        public int ReorderQty { get; set; }

        [Display(Name = "Measure Id")]
        public int MeasuresID { get; set; }

        [Display(Name = "Measurement Unit(e.g. pounds")]
        [ForeignKey("MeasuresID")]
        public Measure Measure { get; set; }

        //Total weight of a single item
        [Display(Name = "Weight")]
        public decimal MeasureAmnt { get; set; }

        //Used to log the username of a modified record
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }

        //Used to log the modified records date
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModifiedDate { get; set; }

        

        //NAVIGATION PROPERTIES
        public virtual ICollection<VendorItem> VendorItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<BuildAssembly> BuildAssemblyList { get; set; }
        
    }
}
