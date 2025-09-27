using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Infrastructure.Data
{
    public class EShopDbContext(DbContextOptions<EShopDbContext> options) : DbContext(options)
    {
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderEntity>(ConfigureOrderEntity);
            modelBuilder.Entity<OrderDetails>(ConfigureOrderDetail);
        }
        public void ConfigureOrderEntity(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.CustomerId).HasMaxLength(64).IsRequired();
            builder.Property(o => o.CustomerName).HasMaxLength(128).IsRequired();
            builder.Property(o => o.PaymentName).HasMaxLength(64);
            builder.Property(o => o.ShippingAddress).HasMaxLength(256).IsRequired();
            builder.Property(o => o.ShippingMethod).HasMaxLength(64);
            builder.Property(o => o.BillAmount).HasColumnType("decimal(18,2)");
            builder.Property(o => o.OrderStatus).HasMaxLength(32);
        }

        public void ConfigureOrderDetail(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("Order_Details");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.ProductId).HasMaxLength(64).IsRequired();
            builder.Property(d => d.ProductName).HasMaxLength(128).IsRequired();
            builder.Property(d => d.Qty).IsRequired();
            builder.Property(d => d.Price).HasColumnType("decimal(18,2)");
            builder.Property(d => d.Discount).HasColumnType("decimal(18,2)");

            builder.HasOne(d => d.OrderEntity)
           .WithMany(o => o.OrderDetails)
           .HasForeignKey(d => d.OrderId);
        }
    }
}
