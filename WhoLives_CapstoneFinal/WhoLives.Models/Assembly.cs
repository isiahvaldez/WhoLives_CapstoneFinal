using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhoLives.Models
{
    /// <summary>
    /// An item and quantity to use in a build assembly
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

        /// <summary>
        /// What build assemblies use this assembly
        /// </summary>
        public virtual ICollection<BuildAssembly> BuildAssemblyList { get; set; }
    }
}
