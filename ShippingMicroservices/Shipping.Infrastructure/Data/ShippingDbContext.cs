using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shipping.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure.Data
{
    public class ShippingDbContext : DbContext
    {
        public ShippingDbContext(DbContextOptions<ShippingDbContext> options) : base(options) { }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<ShipperRegion> ShipperRegions { get; set; }
        public DbSet<ShippingDetails> ShippingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>(ConfigureRegion);
            modelBuilder.Entity<Shipper>(ConfigureShipper);
            modelBuilder.Entity<ShipperRegion>(ConfigureShipperRegion);
            modelBuilder.Entity<ShippingDetails>(ConfigureShippingDetail);
        }

        private void ConfigureRegion(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Region");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .HasMaxLength(128)
                   .IsRequired();
        }

        private void ConfigureShipper(EntityTypeBuilder<Shipper> builder)
        {
            builder.ToTable("Shipper");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .HasMaxLength(128)
                   .IsRequired();

            builder.Property(x => x.EmailId)
                   .HasMaxLength(256);

            builder.Property(x => x.Phone)
                   .HasMaxLength(32);

            builder.Property(x => x.ContactPerson)
                   .HasColumnName("Contact_Person")
                   .HasMaxLength(128);
        }

        private void ConfigureShipperRegion(EntityTypeBuilder<ShipperRegion> builder)
        {
            builder.ToTable("Shipper_Region");

            builder.HasKey(x => new { x.RegionId, x.ShipperId });

            builder.Property(x => x.RegionId)
                   .HasColumnName("Region_Id")
                   .IsRequired();

            builder.Property(x => x.ShipperId)
                   .HasColumnName("Shipper_Id")
                   .IsRequired();

            builder.Property(x => x.Active)
                   .IsRequired();

            builder.HasOne(x => x.Region)
                   .WithMany()
                   .HasForeignKey(x => x.RegionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Shipper)
                   .WithMany()
                   .HasForeignKey(x => x.ShipperId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.RegionId, x.ShipperId })
                   .IsUnique();
        }

        private void ConfigureShippingDetail(EntityTypeBuilder<ShippingDetails> builder)
        {
            builder.ToTable("Shipping_Details");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderId)
                   .HasColumnName("Order_Id")
                   .IsRequired();

            builder.Property(x => x.ShipperId)
                   .HasColumnName("Shipper_Id")
                   .IsRequired();

            builder.Property(x => x.ShippingStatus)
                   .HasColumnName("Shipping_Status")
                   .HasMaxLength(64);

            builder.Property(x => x.TrackingNumber)
                   .HasColumnName("Tracking_Number")
                   .HasMaxLength(64);

            builder.HasOne(x => x.Shipper)
                   .WithMany()
                   .HasForeignKey(x => x.ShipperId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.OrderId);
            builder.HasIndex(x => x.TrackingNumber);
        }
    }
}
