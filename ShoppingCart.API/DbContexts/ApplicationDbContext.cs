using Microsoft.EntityFrameworkCore;
using ShoppingCart.API.Models;

namespace ShoppingCart.API.DbContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<ShoppingCartHeaderModel> ShoppingCartHeaders { get; set; }

    public DbSet<ShoppingCartDetailsModel> ShoppingCartDetails { get; set; }

    public DbSet<ProductModel> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>()
            .HasKey(x => x.Id);
    }
}