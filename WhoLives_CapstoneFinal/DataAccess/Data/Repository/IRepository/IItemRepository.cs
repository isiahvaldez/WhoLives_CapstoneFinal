﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IItemRepository : IRepository<Item>
    {
        IEnumerable<SelectListItem> GetItemListForDropDown();

        void update(Item item);
    }
}
