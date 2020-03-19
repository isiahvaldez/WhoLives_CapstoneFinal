using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private readonly ApplicationDbContext _appContext;
        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
            _appContext = context;
        }
        public void update(OrderItem orderItem)
        {

        }
    }
}
