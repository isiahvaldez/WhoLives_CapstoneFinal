using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.WebEncoders.Testing;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;
using WhoLives.Models.ViewModels;

namespace WhoLives_CapstoneFinal.Controllers
{
    public class Component
    {
        public string id { get; set; }
        public string InventoryItemID { get; set; }
        public string Qty { get; set; }
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BuildAssemblyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BuildAssemblyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("{id}")]
        [ActionName("id")]
        public IActionResult ID(int id)
        {
            // delete all build items for the current id if any exist
            IEnumerable<BuildAssembly> buildList = _unitOfWork.BuildAssemblies.GetAll().Where(a => a.InventoryItemID == id);
            foreach (var build in buildList)
            {
                _unitOfWork.BuildAssemblies.Remove(build);
            }
            _unitOfWork.Save();

            return Json(new { msg = "success" });
        }

        [HttpPost()]
        [ActionName("list")]
        public IActionResult List(Component[] components)
        {
            if (components != null)
            {
                //var json = JsonConvert.SerializeObject(componentList);
                //Component[] components = JsonConvert.DeserializeObject<Component[]>(componentList);
                InventoryItem InventoryItemObj = new InventoryItem();

                if (components.Length > 0 && components[0].id != null)
                {
                    InventoryItemObj = _unitOfWork.InventoryItems.GetFirstOrDefault(u => u.InventoryItemID == Int32.Parse(components[0].id));
                    if (InventoryItemObj == null)
                    {
                        return NotFound();
                    }
                }

                // get current build assembly list for the item
                IEnumerable<BuildAssembly> BuildList = _unitOfWork.BuildAssemblies.GetAll().Where(b => b.InventoryItemID == InventoryItemObj.InventoryItemID);
                IEnumerable<Assembly> assemblyList = _unitOfWork.Assemblies.GetAll();
                BuildAssembly tempBuild = new BuildAssembly();
                Assembly tempAssembly = new Assembly();
                Assembly tempAssemblyWithQty = new Assembly();

                bool assemblyMatch = false; // if an assembly exists with the desired component + qty; we'll use that assembly for the build assembly
                
                foreach (var component in components)
                {
                    // clear the variables and objects we need
                    assemblyMatch= false;
                    tempBuild = new BuildAssembly();
                    tempAssembly = new Assembly();

                    // do any assemblies use this component?
                    IEnumerable<Assembly> possibleAssemblies = assemblyList.Where(a => a.InventoryItemID == Int32.Parse(component.InventoryItemID));

                    if(possibleAssemblies.Count() > 0)
                    {
                        // do any of these assemblies have the same qty desired?
                        tempAssemblyWithQty = possibleAssemblies.SingleOrDefault(a => a.ItemQty == Int32.Parse(component.Qty));
                        if(tempAssemblyWithQty == null)
                        {
                            // if no assembly with that qty exists, create it
                            tempAssemblyWithQty = new Assembly();
                            tempAssemblyWithQty.InventoryItemID = Int32.Parse(component.InventoryItemID);
                            tempAssemblyWithQty.ItemQty = Int32.Parse(component.Qty);
                            _unitOfWork.Assemblies.Add(tempAssemblyWithQty);
                            _unitOfWork.Save();
                        }

                        // Do any of the builds use these assemblies? 
                        foreach (var build in BuildList)
                        {
                            foreach(var assembly in possibleAssemblies)
                            {
                                if(build.AssemblyID == assembly.AssemblyID)
                                {
                                    assemblyMatch = true;
                                    tempAssembly = assembly;
                                    tempBuild = build;
                                    break;
                                }
                            }
                            if (assemblyMatch)
                            {
                                break;
                            }
                        }
                        // yes, a build assembly uses the current component
                        if(assemblyMatch)
                        {
                            // does the amount match the desired amount?
                            if(tempAssembly.ItemQty != tempAssemblyWithQty.ItemQty)
                            {
                                // if no, set the build assembly to use the right amount
                                tempBuild.AssemblyID = tempAssemblyWithQty.AssemblyID;
                                _unitOfWork.BuildAssemblies.update(tempBuild);
                                _unitOfWork.Save();
                            }
                        }
                        // no, the build doesn't use an existing assembly for this item
                        else
                        {
                            // create a new build using the right assembly
                            tempBuild = new BuildAssembly();
                            tempBuild.InventoryItemID = InventoryItemObj.InventoryItemID;
                            tempBuild.AssemblyID = tempAssemblyWithQty.AssemblyID;
                            _unitOfWork.BuildAssemblies.Add(tempBuild);
                        }
                    }
                    else
                    {
                        // no build or assemblies using this component, create a new build and assembly
                        // create a new assembly for this component
                        tempAssembly.InventoryItemID = Int32.Parse(component.InventoryItemID);
                        tempAssembly.ItemQty = Int32.Parse(component.Qty);
                        _unitOfWork.Assemblies.Add(tempAssembly);
                        _unitOfWork.Save();
                        // now create the build
                        tempBuild = new BuildAssembly();
                        tempBuild.InventoryItemID = InventoryItemObj.InventoryItemID;
                        tempBuild.AssemblyID = tempAssembly.AssemblyID;
                        _unitOfWork.BuildAssemblies.Add(tempBuild);
                        _unitOfWork.Save();
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
                        var assembly = assemblyList.SingleOrDefault<Assembly>(a => a.AssemblyID == build.AssemblyID);
                        if (assembly == null || Int32.Parse(component.InventoryItemID) == assembly.InventoryItemID)
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
                }
                _unitOfWork.Save();

                return Json(new { msg = "success" });
            }

            else return Json(new { msg = "Empty list received" });
        }
    }
}