using Product.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Infrastructure.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<CategoryVariation> CategoryVariations { get; set; }
        public DbSet<VariationValue> VariationValues { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductVariationValue> ProductVariationValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>(ConfigureProductCategory);
            modelBuilder.Entity<CategoryVariation>(ConfigureCategoryVariation);
            modelBuilder.Entity<VariationValue>(ConfigureVariationValue);
            modelBuilder.Entity<ProductEntity>(ConfigureProduct);
            modelBuilder.Entity<ProductVariationValue>(ConfigureProductVariationValue);
        }

        private void ConfigureProductCategory(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("Product_Category");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .HasMaxLength(128)
                   .IsRequired();

            builder.HasOne(x => x.Parent)
                   .WithMany()
                   .HasForeignKey(x => x.ParentCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureCategoryVariation(EntityTypeBuilder<CategoryVariation> builder)
        {
            builder.ToTable("Category_Variation");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.VariationName)
                   .HasMaxLength(64)
                   .IsRequired();

            builder.HasOne(x => x.Category)
                   .WithMany(c => c.Variations)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureVariationValue(EntityTypeBuilder<VariationValue> builder)
        {
            builder.ToTable("Variation_Value");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Value)
                   .HasMaxLength(64)
                   .IsRequired();

            builder.HasOne(x => x.Variation)
                   .WithMany(v => v.Values)
                   .HasForeignKey(x => x.VariationId)
                   .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureProduct(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .HasMaxLength(128)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(2048);

            builder.Property(x => x.Price)
                   .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Qty)
                   .IsRequired();

            builder.Property(x => x.ProductImage)
                   .HasMaxLength(256);

            builder.Property(x => x.SKU)
                   .HasMaxLength(64);

            builder.HasIndex(x => x.SKU).IsUnique();

            builder.HasOne(x => x.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureProductVariationValue(EntityTypeBuilder<ProductVariationValue> builder)
        {
            builder.ToTable("Product_Variation_Values");

            builder.HasKey(x => new { x.ProductId, x.VariationValueId });

            builder.HasOne(x => x.Product)
                   .WithMany(p => p.VariationValues)
                   .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.VariationValue)
                   .WithMany(vv => vv.ProductLinks)
                   .HasForeignKey(x => x.VariationValueId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
