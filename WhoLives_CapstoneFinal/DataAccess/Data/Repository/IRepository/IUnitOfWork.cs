using System;
using System.Collections.Generic;
using System.Text;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IMeasuresRepository Measures { get; }
        IPurchaseOrderRepository PurchaseOrders { get; }
        IOrderItemRepository OrderItems { get; }
        void Save();
    }
}
