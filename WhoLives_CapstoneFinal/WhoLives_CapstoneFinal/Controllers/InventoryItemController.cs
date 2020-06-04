using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
                return Json(new { data = _unitOfWork.InventoryItems.GetAll(i => i.IsActive == true) });
            }
            else if (input.Equals("ORDER"))
            {
                var items = _unitOfWork.InventoryItems.GetAll(r => r.IsAssembly != true && r.TotalLooseQty < r.ReorderQty && r.IsActive == true);
                var venditem = _unitOfWork.VendorItems.GetAll();
                var vend = _unitOfWork.Vendors.GetAll(i => i.isActive == true);


                //return Json(new { data = _unitOfWork.InventoryItems.GetAll().Where(r => r.IsAssembly != true && r.TotalLooseQty < r.ReorderQty) });
                return Json(new { data =
                    items.Join(venditem, i => i.InventoryItemID, v => v.InventoryItemID,
                    (i, v) => new { i.InventoryItemID, i.Name, i.TotalLooseQty, i.ReorderQty, v.VendorItemId, v.VendorID }).Join(
                        vend, s => s.VendorID, q => q.VendorID, (s, q) => new { s.InventoryItemID, s.Name, s.TotalLooseQty, s.ReorderQty, q.VendorName })
                });
            }
            else
            {
                return Json(new { data = _unitOfWork.InventoryItems.GetAll().Where(r => r.IsAssembly == true && r.IsActive == true) });
            }
        }

        [HttpPost("{ITEMID,QTY}")]
        [ActionName("check")]        
        public IActionResult check(string? ITEMID, string? QTY)
        {
            int itemid = Int32.Parse(ITEMID);
            int qtyNeeded = 0;
            int qtyAssembled = 1;
            if (QTY != null)
            {
                qtyAssembled = Int32.Parse(QTY);
            }
            bool checking = false;
            // Pull the assembly from the database 
            List<BuildAssembly> assembleList = _unitOfWork.BuildAssemblies.GetAll(a => a.InventoryItemID == itemid).ToList();

            // Loop For Error checking
            foreach (BuildAssembly item in assembleList)
            {
                var assemblyFromDb = _unitOfWork.Assemblies.GetFirstOrDefault(r => r.AssemblyID == item.AssemblyID);
                qtyNeeded = assemblyFromDb.ItemQty;

                var objFromDb = _unitOfWork.InventoryItems.GetFirstOrDefault(u => u.InventoryItemID == assemblyFromDb.InventoryItemID);

                if (qtyNeeded * qtyAssembled > objFromDb.TotalLooseQty && checking != true)
                {
                    // There is not Enough return A notifcation that Not Enough 
                    checking = true;                   
                }
                

                // Loop through the Assemblie recipe and check the qty required for the assembly 
                // Then calculate the QTY needed to make it
                // Check to the QTY needed and compare against 

            }
            if (checking) { 
                return Json(new { success = false, message = "Inventory did not meet recipe for a Item(s). Press confirm to override." });
            }
            else
            {
                return Assemble(QTY, ITEMID, true);
                //return Json(new { success = true, message = "Success" });
            }
        }
        

       [HttpPut("{QTY,ITEMID,ASSEMBLE}")]        
        [ActionName("assemble")]
        public IActionResult Assemble(string? QTY, string? ITEMID, bool? ASSEMBLE)
        {
            int qtyNeeded = 0;
            int qtyAssembled = 1;
            if (QTY != null)
            {
                qtyAssembled = Int32.Parse(QTY);
            }
            bool check = false;
            // ASSEMBLE is a way to seperate Disassemble and Assemble functionality. 
            if (qtyAssembled >0 )
            {
                // pull the recipe for the assembly 
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
                    return Json(new { error = true, message = "Error While Assembling" });
                }
                assemblyItemFromDb.TotalLooseQty += qtyAssembled;

                _unitOfWork.Save();

                //No longer need. SWAL handles
                //if (check)
                //{
                //    return Json(new { success = false, message = "ALERT!!!! Inventory did not meet recipe for a Item(s)" });
                //}
                //else
                //{
                //    return Json(new { success = true, message = "Suceessfully Added " + qtyAssembled + " Assembly to the inventory" });

                //}

                return Json(new { success = true, message = "Suceessfully Added " + qtyAssembled + " Assembly to the inventory" });
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
                    return Json(new { error = true, message = "Error While Assembling" });
                }
                assemblyItemFromDb.TotalLooseQty -= qtyAssembled;

                _unitOfWork.Save();


                return Json(new { success = true, message = "Successfully Disassembled " + qtyAssembled + " assembly to the inventory" });

            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _unitOfWork.InventoryItems.GetFirstOrDefault(u => u.InventoryItemID == id);
            if (item == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            item.IsActive = false;
            _unitOfWork.InventoryItems.Update(item);
            //_unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
            //return RedirectToPage("./Pages/Inventory/Index");
        }

    }
}