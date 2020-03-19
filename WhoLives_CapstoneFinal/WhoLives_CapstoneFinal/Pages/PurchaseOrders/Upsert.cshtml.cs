using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives_CapstoneFinal
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _uow;
        public UpsertModel(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [BindProperty]
        public PurchaseOrder PurchaseOrder { get; set; }
        public IActionResult OnGet(int? id)
        {
            if(id != null)
            {
                PurchaseOrder = _uow.PurchaseOrders.GetFirstOrDefault(p => p.PurchaseOrderID == id);
                if(PurchaseOrder == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }
    }
}