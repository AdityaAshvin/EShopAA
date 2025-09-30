using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promotion.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Infrastructure.Data
{
    public class PromotionDbContext : DbContext
    {
        public PromotionDbContext(DbContextOptions<PromotionDbContext> options) : base(options) { }

        public DbSet<PromotionEntity> Promotions { get; set; }
        public DbSet<PromotionDetails> PromotionDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PromotionEntity>(ConfigurePromotion);
            modelBuilder.Entity<PromotionDetails>(ConfigurePromotionDetails);
        }

        private void ConfigurePromotion(EntityTypeBuilder<PromotionEntity> builder)
        {
            builder.ToTable("Promotion");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .HasMaxLength(128)
                   .IsRequired();

            builder.Property(p => p.Description)
                   .HasMaxLength(1024);

            builder.Property(p => p.Discount)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.Property(p => p.StartDate)
                   .HasColumnName("Start_Date")
                   .IsRequired();

            builder.Property(p => p.EndDate)
                   .HasColumnName("End_Date")
                   .IsRequired();

            builder.HasIndex(p => p.StartDate);
            builder.HasIndex(p => p.EndDate);
        }

        private void ConfigurePromotionDetails(EntityTypeBuilder<PromotionDetails> builder)
        {
            builder.ToTable("Promotion_Details");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.PromotionId)
                   .HasColumnName("Promotion_Id")
                   .IsRequired();

            builder.Property(d => d.ProductCategoryId)
                   .HasColumnName("Product_Category_Id")
                   .IsRequired();

            builder.Property(d => d.ProductCategoryName)
                   .HasColumnName("Product_Category_Name")
                   .HasMaxLength(128)
                   .IsRequired();

            builder.HasOne(d => d.PromotionEntity)
                   .WithMany(p => p.Details)
                   .HasForeignKey(d => d.PromotionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(d => new { d.PromotionId, d.ProductCategoryId }).IsUnique();
        }
    }
}
