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

        public new void Remove(PurchaseOrder entity)
        {
            //Get all the order items associated
            var orderItems = _appContext.OrderItems.Where(o => o.PurchaseOrderID == entity.PurchaseOrderID).ToList();
            foreach(var o in orderItems)
            {
                ////Remove any vendoritems that no longer have any purchase orders with the item
                //var PurchaseOrders = _appContext.PurchaseOrders.Where(v => v.VendorID == entity.VendorID && v. == o.ItemID).ToList();
                
                //_appContext.SaveChanges();
                _appContext.OrderItems.Remove(o);
            }
            _appContext.SaveChanges();
            //Remove the purchase order
            dbSet.Remove(entity);
            _appContext.SaveChanges();
        }

        public void update(PurchaseOrder purchaseOrder)
        {
            var PO = _appContext.PurchaseOrders.FirstOrDefault(p => p.PurchaseOrderID == purchaseOrder.PurchaseOrderID);
            PO.VendorID = purchaseOrder.VendorID;
            PO.StatusChangeDate = PO.StatusID == purchaseOrder.StatusID ? PO.StatusChangeDate : DateTime.Now;
            PO.StatusID = purchaseOrder.StatusID;
            PO.PO = purchaseOrder.PO;
            PO.TotalPrice = purchaseOrder.TotalPrice;
            PO.DateOrdered = purchaseOrder.DateOrdered;
            PO.LastModifiedBy = purchaseOrder.LastModifiedBy;
            PO.LastModifiedDate = DateTime.Now;

            _appContext.SaveChanges();
        }

    }
}
