using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
    {
        void update(PurchaseOrder purchaseOrder);
    }
}
