using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;
using WhoLives.Models.ViewModels;

namespace WhoLives_CapstoneFinal.Controllers
{
    public class ItemsRequired
    {
        public List<InventoryItem> itemList;
        public List<int> requiredQty;
        public List<int> totalQty;
    }

    public class ItemWithCounts
    {
        public int id;
        public string name;
        public int looseQty;
        public int requiredQty;
        public int totalQty;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class DrillAssemblyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        InventoryItem drill; 
        int drillID = 104; // this is hardcoded and will need to be changed if the item is deleted and readded - IV 4/15/2020
        ItemsRequired itemsRequired = new ItemsRequired();
        List<ItemWithCounts> itemList = new List<ItemWithCounts>();

        [BindProperty]
        public BuildAssemblyVM buildAssemblyVM { get; set; }

        public DrillAssemblyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
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
                requiredQty = new List<int>(),
                totalQty = new List<int>()
            };

            foreach (var item in itemsRequired.itemList)
            {
                itemsRequired.requiredQty.Add(0);
                itemsRequired.totalQty.Add(item.TotalLooseQty);
            }
            CountAssemblyComponents(drillID, 1); // this is counting the items required for the drill per its recipe

            foreach(var item in buildAssemblyVM.InventoryItems)
            {
                if(item.IsAssembly)
                {
                    CountChildItems(item.InventoryItemID, item.TotalLooseQty);
                }
            }

            // convert list to an object better structured for JSON

            for(int i = 0; i < itemsRequired.itemList.Count; i++)
            {
                itemList.Add(new ItemWithCounts()
                {
                    id = itemsRequired.itemList[i].InventoryItemID,
                    name = itemsRequired.itemList[i].Name,
                    looseQty = itemsRequired.itemList[i].TotalLooseQty,
                    requiredQty = itemsRequired.requiredQty[i],
                    totalQty = itemsRequired.totalQty[i]
                });
            }

            // TODO: change this to give the info we need - IV 4/15/2020
            string json = JsonConvert.SerializeObject(itemList);
            var result = JsonConvert.DeserializeObject(json);

            return Content(json);
            //return Json(new { data = _unitOfWork.InventoryItems.GetAll().Where(i => i.IsAssembly != true) });

        }

        /// <summary>
        /// Counts the items needed for an assembly, adds results to itemsRequired.requiredQty
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="requiredQty"></param>
        private void CountAssemblyComponents(int itemID, int requiredQty)
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
                    CountAssemblyComponents(currItem.InventoryItemID, assembly.ItemQty);
                }
                else
                {
                    int index = itemsRequired.itemList.FindIndex(i => i.InventoryItemID == assembly.InventoryItemID);
                    itemsRequired.requiredQty[index] += requiredQty * assembly.ItemQty;
                }
            }
        }

        /// <summary>
        /// Counts the items needed for an assembly, adds the results tp itemsRequired.totalQty
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="itemQty"></param>
        private void CountChildItems(int itemID, int itemQty)
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
                    itemsRequired.totalQty[index] += itemQty * assembly.ItemQty;
                }
            }
        }
    }
}