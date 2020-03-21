using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhoLives.Models
{
    public class BuildAssembly
    {
        [Key]
        public int BuildAssemblyID { get; set; }

        //Foreign key to link the BuildAssembly table to the Assembly Table
        [ForeignKey("AssemblyID")]
        public Assembly Assembly { get; set; }
        public int AssemblyID { get; set; }


        //Foreign key to link the InventoryItem table to the Assembly Table
        [ForeignKey("InventoryItemID")]
        public InventoryItem InventoryItem { get; set; }
        public int InventoryItemID { get; set; }
    }
}
