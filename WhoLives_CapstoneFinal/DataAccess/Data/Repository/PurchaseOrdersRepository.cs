using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository
{
    public class PurchaseOrdersRepository : Repository<PurchaseOrders>, IPurchaseOrdersRepository
    {
        private readonly ApplicationDbContext _appContext;
        public PurchaseOrdersRepository(ApplicationDbContext context) : base(context)
        {
            _appContext = context;
        }

        public void Remove(PurchaseOrders entity)
        {
            throw new NotImplementedException();
        }

        public void update(PurchaseOrders purchaseOrders)
        {
            throw new NotImplementedException();
        }

    }
}
