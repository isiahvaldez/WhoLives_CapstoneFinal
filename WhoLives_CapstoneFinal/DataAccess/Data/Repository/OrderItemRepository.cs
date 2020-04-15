using System;
using System.Collections.Generic;
using System.Linq;
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
            var OI = _appContext.OrderItems.FirstOrDefault(o =>
                o.OrderItemID == orderItem.OrderItemID
                && o.PurchaseOrderID == orderItem.PurchaseOrderID
                && o.OrderItemID == orderItem.OrderItemID);
            OI.QuantityOrdered = orderItem.QuantityOrdered;
            OI.QuantityReceived = orderItem.QuantityReceived;
            OI.Price = orderItem.Price;
            OI.DateDelivered = orderItem.DateDelivered;
            OI.LastModifiedBy = orderItem.LastModifiedBy;
            OI.LastModifiedDate = DateTime.Now;

            var InventoryItem = _appContext.InventoryItems.Where(i => i.InventoryItemID == orderItem.ItemID).FirstOrDefault();
            // Find wholesale cost of item
            //InventoryItem.Wholesale = _appContext.OrderItems.Where(o => o.OrderItemID == orderItem.OrderItemID).Select(o => o.Price).Average();
            // Update inventory on hand if the received qty increased
            InventoryItem.TotalLooseQty += (orderItem.QuantityReceived == OI.QuantityReceived ? 0 : orderItem.QuantityReceived - OI.QuantityReceived);
            _appContext.Update(InventoryItem);
            _appContext.SaveChanges();
        }
    }
}
