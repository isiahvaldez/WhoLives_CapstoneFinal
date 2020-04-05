using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models.ViewModels;

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
        public IActionResult Get()
        {
            //if (input.Equals("upsert"))
            //{
            //    return Json(new { data = _uow.OrderItems.GetAll(d => d.PurchaseOrderID == 1) });
            //}
            return Json(new { data = _uow.PurchaseOrders.GetAll(null, null, "Vendor") });
        }

        public class myOrderSelection
        {
            public string Vendor { get; set; }
            public string[] Items { get; set; }
        }
        [HttpPost]
        public IActionResult FromReOrder([FromBody]myOrderSelection Selection)      
        {
            string TestId = Selection.Items[0];
           
            return Ok();
        }
    }
}