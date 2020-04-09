using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventoryItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get(string input)
        {
            // ALL is the default Table 
            // Order is the Re order table
            // The data is a Inpur value from the Ajax call 
            if (input.Equals("ALL"))
            {
                return Json(new { data = _unitOfWork.InventoryItems.GetAll() });
            }
            else if (input.Equals("ORDER"))
            {
                var items = _unitOfWork.InventoryItems.GetAll(r => r.IsAssembly != true && r.TotalLooseQty < r.ReorderQty);
                var venditem = _unitOfWork.VendorItems.GetAll();
                var vend = _unitOfWork.Vendors.GetAll();

                //return Json(new { data = _unitOfWork.InventoryItems.GetAll().Where(r => r.IsAssembly != true && r.TotalLooseQty < r.ReorderQty) });
                return Json(new { data = 
                    items.Join(venditem, i => i.InventoryItemID, v => v.InventoryItemID, 
                    (i, v)=> new {i.InventoryItemID, i.Name,i.TotalLooseQty,i.ReorderQty, v.VendorItemId, v.VendorID}).Join(
                        vend, s=>s.VendorID, q=>q.VendorID,(s,q)=>new { s.InventoryItemID, s.Name, s.TotalLooseQty, s.ReorderQty, q.VendorName }) });
            }
            else
            {
                return Json(new { data = _unitOfWork.InventoryItems.GetAll().Where(r => r.IsAssembly == true) });
            }
        }

        

        [HttpPost("{QTY,ITEMID,ASSEMBLE}")]
        public IActionResult Assemble(string? QTY, string? ITEMID, bool? ASSEMBLE)
        {
            int qtyNeeded = 0;
            int qtyAssembled = 1;
            if (QTY != null)
            {
                qtyAssembled = Int32.Parse(QTY);
            }
            bool check = false;
            // ASSEMBLE is a way to seperate Disassemble and Asemble functionality. 
            if (qtyAssembled >0 )
            {
                // pull the recipe fo the assembly 
                // Check the Qty required to make it and VS the qty on hand 
                // Return the result based on if it can be made 

                // Pull the assembly from the database 
                List<BuildAssembly> assembleList = _unitOfWork.BuildAssemblies.GetAll(a => a.InventoryItemID == Int32.Parse(ITEMID)).ToList();

                // Loop For Error checking
                foreach (BuildAssembly item in assembleList)
                {
                    var assemblyFromDb = _unitOfWork.Assemblies.GetFirstOrDefault(r => r.AssemblyID == item.AssemblyID);
                    qtyNeeded = assemblyFromDb.ItemQty;

                    var objFromDb = _unitOfWork.InventoryItems.GetFirstOrDefault(u => u.InventoryItemID == assemblyFromDb.InventoryItemID);

                    if (qtyNeeded * qtyAssembled > objFromDb.TotalLooseQty && check != true)
                    {
                        // There is not Enough return A notifcation that Not Enough 
                        check = true;
                    }
                    objFromDb.TotalLooseQty -= qtyNeeded * qtyAssembled;

                    // Loop through the Assemblie recipe and check the qty required for the assembly 
                    // Then calculate the QTY needed to make it
                    // Check to the QTY needed and compare against 

                }
                var assemblyItemFromDb = _unitOfWork.InventoryItems.GetFirstOrDefault(u => u.InventoryItemID == Int32.Parse(ITEMID));

                if (assemblyItemFromDb == null)
                {
                    return Json(new { error = true, message = "Error While Assemling" });
                }
                assemblyItemFromDb.TotalLooseQty += qtyAssembled;

                _unitOfWork.Save();

                if (check)
                {
                    return Json(new { success = false, message = "ALERT!!!! Inventory did not meet recipe for a Item(s)" });
                }
                else
                {
                    return Json(new { success = true, message = "Suceessfully Added " + qtyAssembled + " Assembly to the inventory" });

                }
            }
            else
            {
                int qtyReplaced = 0;
                qtyAssembled*=-1;


                // pull the recipe fo the assembly 
                // Check the Qty required to make it and VS the qty on hand 
                // Return the result based on if it can be made 

                // Pull the assembly from the database 
                List<BuildAssembly> assembleList = _unitOfWork.BuildAssemblies.GetAll(a => a.InventoryItemID == Int32.Parse(ITEMID)).ToList();

                // Loop For Error checking
                foreach (BuildAssembly item in assembleList)
                {
                    var assemblyFromDb = _unitOfWork.Assemblies.GetFirstOrDefault(r => r.AssemblyID == item.AssemblyID);
                    qtyReplaced = assemblyFromDb.ItemQty;

                    var objFromDb = _unitOfWork.InventoryItems.GetFirstOrDefault(u => u.InventoryItemID == assemblyFromDb.InventoryItemID);

                    objFromDb.TotalLooseQty += qtyReplaced * qtyAssembled;

                    // Loop through the Assemblie recipe and check the qty required for the assembly 
                    // Then calculate the QTY needed to make it
                    // Check to the QTY needed and compare against 

                }
                var assemblyItemFromDb = _unitOfWork.InventoryItems.GetFirstOrDefault(u => u.InventoryItemID == Int32.Parse(ITEMID));

                if (assemblyItemFromDb == null)
                {
                    return Json(new { error = true, message = "Error While Assemling" });
                }
                assemblyItemFromDb.TotalLooseQty -= qtyAssembled;

                _unitOfWork.Save();


                return Json(new { success = true, message = "Suceessfully Disassembled " + qtyAssembled + " assembly to the inventory" });

            }

        }


    
    }
}