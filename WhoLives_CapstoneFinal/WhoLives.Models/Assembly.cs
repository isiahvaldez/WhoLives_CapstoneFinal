using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhoLives.Models
{
    /// <summary>
    /// A component of a Build Assembly
    /// </summary>
    public class Assembly
    {
        [Key]
        public int AssemblyID { get; set; }
        
        public int? InventoryItemID { get; set; }
        /// <summary>
        /// The item being used as a component
        /// </summary>
        [ForeignKey("InventoryItemID")]
        public InventoryItem InventoryItem { get; set; }

        /// <summary>
        /// Number of a certain item to complete an assembly
        /// </summary>
        [Display(Name = "Item Qty")]
        public int ItemQty { get; set; }

        public virtual ICollection<BuildAssembly> BuildAssemblyList { get; set; }
    }
}
