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
        public InventoryItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.InventoryItems.GetAll() });
        }
    }
}