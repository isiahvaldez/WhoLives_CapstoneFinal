using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WhoLives.DataAccess.Data.Repository.IRepository;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository
{
    public class PurchaseOrderRepository : Repository<PurchaseOrder>, IPurchaseOrderRepository
    {
        private readonly ApplicationDbContext _appContext;
        public PurchaseOrderRepository(ApplicationDbContext context) : base(context)
        {
            _appContext = context;
        }

        public void Remove(PurchaseOrder entity)
        {
            throw new NotImplementedException();
        }

        public void update(PurchaseOrder purchaseOrder)
        {
            throw new NotImplementedException();
        }

    }
}
