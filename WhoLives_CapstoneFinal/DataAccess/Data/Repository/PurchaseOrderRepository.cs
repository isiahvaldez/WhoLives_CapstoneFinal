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
            var PO = _appContext.PurchaseOrders.FirstOrDefault(p => p.PurchaseOrderID == purchaseOrder.PurchaseOrderID);
            PO.VendorID = purchaseOrder.VendorID;
            if(PO.Status != purchaseOrder.Status)
            {
                PO.StatusChangeDate = DateTime.Now;
            }
            else
            {
                PO.StatusChangeDate = purchaseOrder.StatusChangeDate;
            }
            PO.Status = purchaseOrder.Status;
            PO.PO = purchaseOrder.PO;
            PO.DateOrdered = purchaseOrder.DateOrdered;
            PO.LastModifiedBy = purchaseOrder.LastModifiedBy;
            PO.LastModifiedDate = DateTime.Now;

            _appContext.SaveChanges();
        }

    }
}
