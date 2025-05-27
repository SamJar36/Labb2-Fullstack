using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Labb2_REST_API.Models;

public partial class FullstackContext : DbContext
{
    public FullstackContext()
    {
    }

    public FullstackContext(DbContextOptions<FullstackContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(c => c.LastName).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Email).IsRequired().HasMaxLength(150);
            entity.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(20);
            entity.Property(c => c.Address).IsRequired().HasMaxLength(250);

            entity.HasMany(c => c.ShoppingCartItems)
                  .WithOne(sci => sci.Customer)
                  .HasForeignKey(sci => sci.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(e => e.Id).UseIdentityColumn(60, 1);

            entity.Property(p => p.ProductName).IsRequired().HasMaxLength(100);
            entity.Property(p => p.ProductDescription).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(p => p.ProductCategory).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Status).IsRequired().HasMaxLength(20);
        });

        modelBuilder.Entity<ShoppingCartItem>(entity =>
        {
            entity.HasKey(sci => sci.Id);
            entity.Property(sci => sci.Id).UseIdentityColumn(1, 1);

            entity.Property(sci => sci.Quantity).IsRequired();

            entity.HasOne(sci => sci.Customer)
                  .WithMany(c => c.ShoppingCartItems)
                  .HasForeignKey(sci => sci.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sci => sci.Product)
                  .WithMany() 
                  .HasForeignKey(sci => sci.ProductId)
                  .OnDelete(DeleteBehavior.Restrict); 
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).UseIdentityColumn(1, 1);

            entity.Property(e => e.OrderDate).IsRequired();
            entity.Property(e => e.Status)
                  .IsRequired()
                  .HasConversion<string>();

            entity.HasOne(e => e.Customer)
                  .WithMany(u => u.Orders)
                  .HasForeignKey(e => e.CustomerId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(e => e.OrderProducts)
                  .WithOne(op => op.Order)
                  .HasForeignKey(op => op.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.Price)
                  .IsRequired()
                  .HasPrecision(18, 2);

            entity.HasOne(e => e.Order)
                  .WithMany(e => e.OrderProducts)
                  .HasForeignKey(e => e.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Relation till Product (Restrict – produkten får inte tas bort om den används)
            entity.HasOne(e => e.Product)
                  .WithMany(e => e.OrderProducts)
                  .HasForeignKey(e => e.ProductId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
