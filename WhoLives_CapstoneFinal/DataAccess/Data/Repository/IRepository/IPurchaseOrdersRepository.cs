using System;
using System.Collections.Generic;
using System.Text;
using WhoLives.Models;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IPurchaseOrdersRepository : IRepository<PurchaseOrders>
    {
        void update(PurchaseOrders purchaseOrders);
    }
}
