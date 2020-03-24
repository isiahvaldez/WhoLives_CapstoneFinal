using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IAssemblyRepository : IRepository<Assembly>
    {
        IEnumerable<SelectListItem> GetItemListForDropDown();

        void update(Assembly assembly);
    }
}
