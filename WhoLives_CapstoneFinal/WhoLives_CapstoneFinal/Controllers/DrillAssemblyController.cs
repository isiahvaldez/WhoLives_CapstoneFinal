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
    public class DrillAssemblyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DrillAssemblyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get(string input)
        {
            if (input != null && input.Equals("INVENTORY"))
            {
                return Json(new { data = _unitOfWork.InventoryItems.GetAll().Where(i => i.IsAssembly != true) });
            }
            else if (input != null && input.Equals("ASSEMBLY"))
            {
                return Json(new { data = _unitOfWork.InventoryItems.GetAll().Where(i => i.IsAssembly == true) });
            }
            else { return Json(new { data = _unitOfWork.InventoryItems.GetAll() }); }
        }
    }
}