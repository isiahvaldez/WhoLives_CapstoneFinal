using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives_CapstoneFinal
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;
        public IEnumerable<PurchaseOrder> PurchaseOrders { get; set; }
        public IndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void OnGet()
        {
            PurchaseOrders = _uow.PurchaseOrders.GetAll(null, null, null);
        }
    }
}