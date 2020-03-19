using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhoLives.Models
{
    public class InventoryItems
    {
        //Primary key for the Item table
        [Key]
        public int InventoryItemID { get; set; }

        //Identifier if the part is used to create an assembly
        public bool AddedAsPartOfAssembly { get; set; }

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

        [Display(Name = "Wholesale Cost")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal ListWholesaleCost { get; set; }

        //Minimum number of parts to have available
        [Display(Name = "Reorder Quantity")]
        public int ReorderQty { get; set; }

        //Maximum number of parts to have available
        [Display(Name = "Maximum Quantity")]
        public int MaxQty { get; set; }

        //Foreign key for the measurement identifier
        [ForeignKey("Measures")]
        [Display(Name = "Measure Id")]
        public int MeasureID { get; set; }

        //Total weight of a single item
        [Display(Name = "Weight")]
        public decimal MeasureAmnt { get; set; }

        //Used to log the username of a modified record
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }

        public int TempRequired { get; set; }

        //Used to log the modified records date
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModifiedDate { get; set; }

        //NAVIGATION PROPERTIES
        //public virtual ICollection<RecipeLine> RecipeLines { get; set; }
        //public virtual ICollection<AssemblyRecipe> AssemblyRecipes { get; set; }
        //public virtual ICollection<OrderItem> OrderItems { get; set; }

        [Display(Name = "Measurement Unit(e.g. pounds")]
        public virtual Measures Measure { get; set; }
    }
}
