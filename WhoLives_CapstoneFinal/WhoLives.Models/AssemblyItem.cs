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
    public class AssemblyItem
    {
        [Key]
        public  int AssemblyItemID { get; set; }
        /// <summary>
        /// The assembly this item belongs to
        /// </summary>
        [ForeignKey("AssemblyItem")]
        public int AssemblyID { get; set; }
        /// <summary>
        /// The item used in the assembly
        /// </summary>
        [ForeignKey("Item")]
        public int ItemID { get; set; }
        /// <summary>
        /// How many of this item are needed for an assembly
        /// </summary>
        [Display(Name = "Item Qty")]
        public int ItemQty { get; set; }
    }
}
