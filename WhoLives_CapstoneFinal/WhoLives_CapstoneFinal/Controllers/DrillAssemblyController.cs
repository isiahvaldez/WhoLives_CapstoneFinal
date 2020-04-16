using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;
using WhoLives.Models.ViewModels;

namespace WhoLives_CapstoneFinal.Controllers
{
    public class ItemsRequired
    {
        public List<InventoryItem> itemList;
        public List<int> requiredQty;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class DrillAssemblyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        InventoryItem drill; 
        int drillID = 104; // this is hardcoded and will need to be changed if the item is deleted and readded - IV 4/15/2020
        ItemsRequired itemsRequired = new ItemsRequired();

        [BindProperty]
        public BuildAssemblyVM buildAssemblyVM { get; set; }

        public DrillAssemblyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get(string input)
        {
            drill = _unitOfWork.InventoryItems.GetFirstOrDefault(i => i.InventoryItemID == drillID);

            // used for general information without needing to query the DB again
            buildAssemblyVM = new BuildAssemblyVM()
            {
                InventoryItems = _unitOfWork.InventoryItems.GetAll(),
                BuildAssemblies = _unitOfWork.BuildAssemblies.GetAll(),
                Assemblies = _unitOfWork.Assemblies.GetAll()
            };

            drill = _unitOfWork.InventoryItems.GetFirstOrDefault(i => i.InventoryItemID == drillID);
            // Initialize the drill's list of items and number required for the drill
            // start with items with 0 required
            itemsRequired = new ItemsRequired()
            {
                itemList = _unitOfWork.InventoryItems.GetAll().Where(i => i.IsAssembly != true).ToList(),
                requiredQty = new List<int>()
            };

            foreach (var item in itemsRequired.itemList)
            {
                itemsRequired.requiredQty.Add(0);
            }
            CountChildItems(drillID, 1);

            // TODO: change this to give the info we need - IV 4/15/2020
            return Json(new { data = _unitOfWork.InventoryItems.GetAll().Where(i => i.IsAssembly != true) });

        }

        // this is used for finding the required number of items to build the drill
        private void CountChildItems(int itemID, int requiredQty)
        {
            // build list for this item
            List<BuildAssembly> buildAssemblies =
                _unitOfWork.BuildAssemblies.GetAll().Where(i => i.InventoryItemID == itemID).ToList();
            // get assemblies these builds reference
            List<Assembly> assemblyList = new List<Assembly>();
            foreach (var build in buildAssemblies)
            {
                assemblyList.Add(_unitOfWork.Assemblies.GetFirstOrDefault(i => i.AssemblyID == build.AssemblyID));
            }
            // go through each assembly
            foreach (var assembly in assemblyList)
            {
                InventoryItem currItem = buildAssemblyVM.InventoryItems.ToList().Find(i => i.InventoryItemID == assembly.InventoryItemID);
                if (currItem.IsAssembly)
                {
                    CountChildItems(currItem.InventoryItemID, assembly.ItemQty);
                }
                else
                {
                    int index = itemsRequired.itemList.FindIndex(i => i.InventoryItemID == assembly.InventoryItemID);
                    itemsRequired.requiredQty[index] += requiredQty * assembly.ItemQty;
                }
            }
        }
    }
}