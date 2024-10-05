using FashionStore.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Cart> carts { get; set; }
    public DbSet<CartItem> cartItems { get; set; }
    public DbSet<Category> categories { get; set; }
    public DbSet<Coupon> coupons { get; set; }
    public DbSet<Order> orders { get; set; }
    public DbSet<OrderItem> orderItems { get; set; }
    public DbSet<Payment> payments { get; set; }
    public DbSet<Product> products { get; set; }
    public DbSet<Color> colors { get; set; }
    public DbSet<Size> sizes { get; set; }
    public DbSet<Review> reviews { get; set; }
    public DbSet<ShippingInfo> shippingInfos { get; set; }
    public DbSet<User> users { get; set; }
    public DbSet<Wishlist> wishlists { get; set; }
    public DbSet<WishlistItem> wishlistItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.CategoryId);
    }
}
