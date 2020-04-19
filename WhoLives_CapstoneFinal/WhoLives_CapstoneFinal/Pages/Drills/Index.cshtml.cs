using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;
using WhoLives.Models.ViewModels;

namespace WhoLives_CapstoneFinal.Pages.Drills
{
    [Authorize]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int numToAssemble { get; set; }

        public InventoryItem drill;
        int drillID = 104; // this is hardcoded and will need to be changed if the item is deleted and readded - IV 4/15/2020

        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            drill = _unitOfWork.InventoryItems.GetFirstOrDefault(i => i.InventoryItemID == drillID);
        }

        public IActionResult OnPost()
        {
            if (numToAssemble != 0)
            {
                // assemble or disassemble the number of drills
                drill = _unitOfWork.InventoryItems.GetFirstOrDefault(i => i.InventoryItemID == drillID);
                List<BuildAssembly> buildAssemblies = _unitOfWork.BuildAssemblies.GetAll().Where(i => i.InventoryItemID == drillID).ToList();
                List<Assembly> assemblies = new List<Assembly>();
                List<InventoryItem> inventoryItems = new List<InventoryItem>();
                foreach (var build in buildAssemblies)
                {
                    assemblies.Add(_unitOfWork.Assemblies.GetFirstOrDefault(i => i.AssemblyID == build.AssemblyID));
                }
                foreach (var assembly in assemblies)
                {
                    inventoryItems.Add(_unitOfWork.InventoryItems.GetFirstOrDefault(i => i.InventoryItemID == assembly.InventoryItemID));
                }
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    // a positive number of drills to assemble will decrease the components counts
                    inventoryItems[i].TotalLooseQty -= (assemblies[i].ItemQty * numToAssemble);
                    _unitOfWork.InventoryItems.Update(inventoryItems[i]);
                }
                drill.TotalLooseQty += numToAssemble;
                _unitOfWork.InventoryItems.Update(drill);
                _unitOfWork.Save();
            }
            return Page();
        }
    }
}