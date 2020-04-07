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
    public class VendorOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendorOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{vendorID}")]
        public IActionResult Get(int? vendorID)
        {
            if (vendorID != null)
            {
                return Json(new { data = _unitOfWork.PurchaseOrders.GetAll().Where(o => o.VendorID == vendorID) });
            }
            else
            {
                return Json(new { } ); // a null ID will be handled in the upsert cshtml - IV 4/3/2020
            }
        }
    }
}