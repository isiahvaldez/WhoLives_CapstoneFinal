using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhoLives.Models
{
    /// <summary>
    /// One of the items necessary to build a given assembly
    /// </summary>
    public class Assembly
    {
        [Key]
        public int AssemblyID { get; set; }
        
        //Foreign key to link the InventoryItem table to the Assembly Table
        public int InventoryItemID { get; set; }
        [ForeignKey("InventoryItemID")]
        public InventoryItem InventoryItem { get; set; }

        /// <summary>
        /// Number of items used for an individual assembly.
        /// </summary>
        [Display(Name = "Item Qty")]
        public int ItemQty { get; set; }

        public virtual ICollection<BuildAssembly> BuildAssemblyList { get; set; }
    }
}
