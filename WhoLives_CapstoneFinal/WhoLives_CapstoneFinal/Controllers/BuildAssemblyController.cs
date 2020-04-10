using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.WebEncoders.Testing;
using Newtonsoft.Json;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;
using WhoLives.Models.ViewModels;

namespace WhoLives_CapstoneFinal.Controllers
{
    public class Component
    {
        public string id;
        public string InventoryItemID;
        public string Qty;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class BuildAssemblyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BuildAssemblyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("{componentList}")]
        public IActionResult Post(string componentList)
        {
            if (componentList != null)
            {
                Component[] components = JsonConvert.DeserializeObject<Component[]>(componentList);

                // use inventoryItemVM to compare existing list vs new list
                InventoryItemVM InventoryItemVM = new InventoryItemVM
                {
                    OrderInfo = new PurchaseOrder(),
                    ItemList = _unitOfWork.InventoryItems.GetItemListForDropDown(),
                    MeasureInfo = _unitOfWork.Measures.GetMeasureListForDropDown(),
                    BuildInfo = new BuildAssembly(),
                    AssemblyInfo = new Assembly(),
                    InventoryItemObj = new InventoryItem()
                };

                if (components[0].id != null)
                {
                    InventoryItemVM.InventoryItemObj = _unitOfWork.InventoryItems.GetFirstOrDefault(u => u.InventoryItemID == Int32.Parse(components[0].id));
                    if (InventoryItemVM.InventoryItemObj == null)
                    {
                        return NotFound();
                    }
                }
                // get current build assembly list for the item
                IEnumerable<BuildAssembly> BuildList = _unitOfWork.BuildAssemblies.GetAll().Where(b => b.InventoryItemID == Int32.Parse(components[0].id));
                IEnumerable<Assembly> assemblyList = _unitOfWork.Assemblies.GetAll();
                BuildAssembly tempBuild = new BuildAssembly();
                Assembly tempAssembly = new Assembly();
                bool buildMatch = false; // if a build for the assembly + item combo exists
                bool assemblyMatch = false; // if an assembly exists with the desired component + qty
                bool itemMatch = false;
                int assemblyID; // used for finding a build + assembly combo
                
                foreach (var component in components)
                {
                    buildMatch = false;
                    assemblyMatch= false;
                    itemMatch = false;
                    // do any assemblies use this component?
                    IEnumerable<Assembly> possibleAssemblies = assemblyList.Where(a => a.InventoryItemID == Int32.Parse(component.InventoryItemID));
                    if(possibleAssemblies.Count() > 0)
                    {
                        // Do any of the builds use these assemblies? 
                        foreach(var build in BuildList)
                        {
                            foreach(var assembly in possibleAssemblies)
                            {
                                if(build.AssemblyID == assembly.AssemblyID)
                                {
                                    assemblyMatch = true;
                                    tempAssembly = assembly;
                                    break;
                                }
                            }
                            if (assemblyMatch)
                            {
                                tempBuild = build;
                                break;
                            }
                        }
                        // yes, a build assembly uses the current component
                        if(assemblyMatch)
                        {
                            // does the amount match the desired amount?
                            if(tempAssembly.ItemQty == Int32.Parse(component.Qty))
                            {
                                buildMatch = true; // nothing more to do here
                            }
                            else
                            {
                                // set the build assembly to use the temp assembly
                                tempBuild.AssemblyID = tempAssembly.AssemblyID;
                                _unitOfWork.BuildAssemblies.update(tempBuild);
                                _unitOfWork.Save();
                            }
                        }
                        // no, the build doesn't use an existing assembly for this item
                        else
                        {
                            // create a new assembly for this component
                            tempAssembly = new Assembly();
                            tempAssembly.InventoryItemID = Int32.Parse(component.InventoryItemID);
                            tempAssembly.ItemQty = Int32.Parse(component.Qty);
                            _unitOfWork.Assemblies.Add(tempAssembly);
                            _unitOfWork.Save();
                            // now create the build
                            tempBuild = new BuildAssembly();
                            tempBuild.InventoryItemID = Int32.Parse(component.id);
                            tempBuild.AssemblyID = tempAssembly.AssemblyID;
                            _unitOfWork.BuildAssemblies.Add(tempBuild);
                        }
                    }
                    else
                    {
                        // no build or assemblies using this component, create a new build and assembly
                        // create a new assembly for this component
                        tempAssembly = new Assembly();
                        tempAssembly.InventoryItemID = Int32.Parse(component.InventoryItemID);
                        tempAssembly.ItemQty = Int32.Parse(component.Qty);
                        _unitOfWork.Assemblies.Add(tempAssembly);
                        _unitOfWork.Save();
                        // now create the build
                        tempBuild = new BuildAssembly();
                        tempBuild.InventoryItemID = Int32.Parse(component.id);
                        tempBuild.AssemblyID = tempAssembly.AssemblyID;
                        _unitOfWork.BuildAssemblies.Add(tempBuild);
                    }
                }
                // now check what's missing from the build list
                bool match = false;
                List<BuildAssembly> buildsToDelete = new List<BuildAssembly>();
                foreach(var build in BuildList)
                {
                    match = false;
                    foreach(var component in components)
                    {
                        if(Int32.Parse(component.InventoryItemID) == build.InventoryItemID)
                        {
                            match = true;
                            break;
                        }
                    }
                    if(match == false) {
                        buildsToDelete.Add(build);
                    }
                }
                // remove build assemblies with no match
                foreach(var build in buildsToDelete)
                {
                    _unitOfWork.BuildAssemblies.Remove(build);
                    _unitOfWork.Save();
                }

                return Json(JsonConvert.SerializeObject(components));
            }
            else return Json(new { msg = "Empty list received" });
        }
    }
}