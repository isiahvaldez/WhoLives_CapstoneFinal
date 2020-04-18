using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;
using WhoLives.Models.ViewModels;

namespace WhoLives_CapstoneFinal.Pages.Drills
{
    [Authorize]
    public class IndexModel : PageModel
    {
        

        public InventoryItem drill;
        int drillID = 104; // this is hardcoded and will need to be changed if the item is deleted and readded - IV 4/15/2020
        

        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            drill = _unitOfWork.InventoryItems.GetFirstOrDefault(i => i.InventoryItemID == drillID);
        }

        
    }
}