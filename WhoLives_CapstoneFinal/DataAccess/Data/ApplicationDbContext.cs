using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhoLives.Models;

namespace WhoLives.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Measure> Measures { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Assembly> Assembly { get; set; }
        public DbSet<BuildAssembly> BuildAssemblies { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<VendorItem> VendorItems{ get; set; }
        
    }
}
