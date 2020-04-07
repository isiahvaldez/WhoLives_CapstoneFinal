using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhoLives.Models
{
    public class BuildAssembly
    {
        /// <summary>
        /// Object that links an inventory item to its assembly components
        /// </summary>
        [Key]
        public int BuildAssemblyID { get; set; }

        /// <summary>
        /// One of the assembly components
        /// </summary>
        [ForeignKey("AssemblyID")]
        public Assembly Assembly { get; set; }
        public int AssemblyID { get; set; }


        /// <summary>
        /// The inventory item that the assembly components will build
        /// </summary>
        [ForeignKey("InventoryItemID")]
        public InventoryItem InventoryItem { get; set; }
        public int InventoryItemID { get; set; }
    }
}
