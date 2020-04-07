using System;
using System.Collections.Generic;
using System.Text;

namespace WhoLives.Models.ViewModels
{
    class DrillAssemblyViewModel
    {
        IEnumerable<InventoryItem> ItemList;
        IEnumerable<Assembly> AssemblyList;
        IEnumerable<BuildAssembly> BuildAssemblyList;
    }
}
