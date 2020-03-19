using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        void update(OrderItem orderItem);
    }
}
