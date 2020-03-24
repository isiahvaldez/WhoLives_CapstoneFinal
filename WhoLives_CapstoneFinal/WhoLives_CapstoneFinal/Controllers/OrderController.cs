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
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _uow;

        public OrderController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [HttpGet]
        public IActionResult Get() => Json(new { data = _uow.PurchaseOrders.GetAll(null, null, "Vendor") });
    }
}