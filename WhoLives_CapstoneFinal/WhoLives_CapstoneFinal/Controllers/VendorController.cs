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
            return Json(new { data = _unitOfWork.Vendors.GetAll(i => i.isActive == true) });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vendor = _unitOfWork.Vendors.GetFirstOrDefault(u => u.VendorID == id);
            if (vendor == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            vendor.isActive = false;
            _unitOfWork.Vendors.Update(vendor);
            //_unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
            //return RedirectToPage("./Pages/Inventory/Index");
        }
    }
}