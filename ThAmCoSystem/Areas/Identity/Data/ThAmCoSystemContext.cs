using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThAmCoSystem.Areas.Identity.Data;
using ThAmCoSystem.Models.AccountBalances;
using ThAmCoSystem.Models.OrderItems;
using ThAmCoSystem.Models.Orders;
using ThAmCoSystem.Models.OrdersHistory;
using ThAmCoSystem.Models.Products;

namespace ThAmCoSystem.Areas.Identity.Data;

public class ThAmCoSystemContext : IdentityDbContext<ThAmCoSystemUser>
{
    public ThAmCoSystemContext(DbContextOptions<ThAmCoSystemContext> options)
        : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<AccountBalance> AccountBalances { get; set; }
    public DbSet<OrderHistory> OrdersHistory { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
