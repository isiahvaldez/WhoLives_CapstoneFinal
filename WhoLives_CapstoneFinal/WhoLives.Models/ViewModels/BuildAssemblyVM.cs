using System;
using System.Collections.Generic;
using System.Text;

namespace WhoLives.Models.ViewModels
{
    public class BuildAssemblyVM
    {
        public IEnumerable<InventoryItem> InventoryItems { get; set; }
        public IEnumerable<BuildAssembly> BuildAssemblies { get; set; }
        public IEnumerable<Assembly> Assemblies { get; set; }
        public InventoryItem InventoryItem { get; set; }
    }
}
