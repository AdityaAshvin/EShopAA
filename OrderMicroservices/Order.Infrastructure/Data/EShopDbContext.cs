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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderEntity>(ConfigureOrderEntity);
            modelBuilder.Entity<OrderDetails>(ConfigureOrderDetail);
            modelBuilder.Entity<Customer>(ConfigureCustomer);
            modelBuilder.Entity<Address>(ConfigureAddress);
            modelBuilder.Entity<UserAddress>(ConfigureUserAddress);
            modelBuilder.Entity<PaymentType>(ConfigurePaymentType);
            modelBuilder.Entity<PaymentMethod>(ConfigurePaymentMethod);
            modelBuilder.Entity<ShoppingCart>(ConfigureCart);
            modelBuilder.Entity<ShoppingCartItem>(ConfigureCartItem);
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
        public void ConfigureCustomer(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(64).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(64).IsRequired();
            builder.Property(x => x.Gender).HasMaxLength(16);
            builder.Property(x => x.PhoneNum).HasMaxLength(32);
            builder.Property(x => x.ProfilePic).HasMaxLength(256);
            builder.Property(x => x.UserId).HasMaxLength(128);
        }
        public void ConfigureAddress(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Street1).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Street2).HasMaxLength(128);
            builder.Property(x => x.City).HasMaxLength(64).IsRequired();
            builder.Property(x => x.State).HasMaxLength(64).IsRequired();
            builder.Property(x => x.ZipCode).HasMaxLength(16).IsRequired();
            builder.Property(x => x.Country).HasMaxLength(64).IsRequired();
        }

        private void ConfigureUserAddress(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("User_Address");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Customer)
             .WithMany(c => c.UserAddresses)
             .HasForeignKey(x => x.CustomerId);

            builder.HasOne(x => x.Address)
             .WithMany(a => a.UserAddresses)
             .HasForeignKey(x => x.AddressId);

            builder.Property(x => x.IsDefaultAddress).HasDefaultValue(false);
        }
        public void ConfigurePaymentType(EntityTypeBuilder<PaymentType> builder)
        {
            builder.ToTable("Payment_Type");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(64).IsRequired();
        }

        public void ConfigurePaymentMethod(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("Payment_Method");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Provider).HasMaxLength(64).IsRequired();
            builder.Property(x => x.AccountNumber).HasMaxLength(64);
            builder.HasOne(x => x.PaymentType)
             .WithMany(t => t.PaymentMethods)
             .HasForeignKey(x => x.PaymentTypeId);
        }

        private void ConfigureCart(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("Shopping_Cart");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CustomerName).HasMaxLength(128).IsRequired();

            builder.HasOne(x => x.Customer)
             .WithMany(c => c.Carts)
             .HasForeignKey(x => x.CustomerId);
        }
        public void ConfigureCartItem(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.ToTable("Shopping_Cart_Item");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProductName).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.ShoppingCart)
             .WithMany(c => c.Items)
             .HasForeignKey(x => x.CartId);
        }
    }
}
