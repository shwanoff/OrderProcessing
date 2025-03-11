using Microsoft.EntityFrameworkCore;
using OrderProcessing.Infrastructure.Entities;

namespace OrderProcessing.Infrastructure
{
    public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
	{
		public DbSet<OrderEntity> Orders { get; set; }
		public DbSet<ProductEntity> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<OrderEntity>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Address).IsRequired();
				entity.Property(e => e.Email).IsRequired();
				entity.Property(e => e.CreditCard).IsRequired();
				entity.HasMany(e => e.Products).WithOne(p => p.Order).HasForeignKey(p => p.OrderId).OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<ProductEntity>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Name).IsRequired();
				entity.Property(e => e.Amount).IsRequired();
				entity.Property(e => e.Price).IsRequired();
			});
		}
	}
}
