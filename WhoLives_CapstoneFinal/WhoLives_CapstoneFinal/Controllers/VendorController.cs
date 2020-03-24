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
    public class VendorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Vendors.GetAll() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Vendors.GetFirstOrDefault(v => v.VendorID == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Vendors.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });

        }
    }
}