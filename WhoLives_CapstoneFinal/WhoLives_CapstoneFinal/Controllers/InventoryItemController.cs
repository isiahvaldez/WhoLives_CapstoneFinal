using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoLives.DataAccess.Data.Repository.IRepository;

namespace WhoLives_CapstoneFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public int ID { get; set; }
        public int quantity { get; set; }

        public InventoryItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get(string input)
        {
            if (input.Equals("ALL"))
            {
                return Json(new { data = _unitOfWork.InventoryItems.GetAll() });
            }
            else if (input.Equals("ORDER"))
            {
                return Json(new { data = _unitOfWork.InventoryItems.GetAll().Where(r => r.IsAssembly != true && r.TotalLooseQty < r.ReorderQty) });
            }
            else
            {
                return Json(new { data = _unitOfWork.InventoryItems.GetAll().Where(r => r.IsAssembly == true) });
            }
        }       
       
        [HttpPost("{id}")]
        public IActionResult Assemble(string temp)
        {
            var objFromDb = _unitOfWork.InventoryItems.GetFirstOrDefault(u => u.InventoryItemID == Int32.Parse(temp));
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error While Locking / Unlocking" });
            }
         
            _unitOfWork.Save();

            return Json(new { success = true, message = "Lock / Unlock Successful" });
        }


    }
}