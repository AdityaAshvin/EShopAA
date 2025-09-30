using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Infrastructure.Data
{
    public class ReviewDbContext : DbContext
    {
        public ReviewDbContext(DbContextOptions<ReviewDbContext> options) : base(options) { }
        public DbSet<CustomerReview> CustomerReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerReview>(ConfigureCustomerReview);
        }

        private void ConfigureCustomerReview(EntityTypeBuilder<CustomerReview> builder)
        {
            builder.ToTable("Customer_Review");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CustomerId)
                   .HasColumnName("Customer_Id")
                   .IsRequired();

            builder.Property(x => x.CustomerName)
                   .HasColumnName("Customer_Name")
                   .HasMaxLength(128)
                   .IsRequired();

            builder.Property(x => x.OrderId)
                   .HasColumnName("Order_Id")
                   .IsRequired();

            builder.Property(x => x.OrderDate)
                   .HasColumnName("Order_Date")
                   .IsRequired();

            builder.Property(x => x.ProductId)
                   .HasColumnName("Product_Id")
                   .IsRequired();

            builder.Property(x => x.ProductName)
                   .HasColumnName("Product_Name")
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.RatingValue)
                   .HasColumnName("Rating_value")
                   .IsRequired();

            builder.Property(x => x.Comment)
                   .HasColumnName("Comment")
                   .HasMaxLength(2000);

            builder.Property(x => x.ReviewDate)
                   .HasColumnName("Review_Date")
                   .IsRequired();

            builder.HasIndex(x => x.ProductId);
            builder.HasIndex(x => new { x.ProductId, x.ReviewDate });
            builder.HasIndex(x => x.CustomerId);
        }
    }
}
