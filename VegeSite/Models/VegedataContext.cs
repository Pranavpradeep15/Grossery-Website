using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VegeSite.Models;

public partial class VegedataContext : DbContext
{
    public VegedataContext()
    {
    }

    public VegedataContext(DbContextOptions<VegedataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<Vegetable> Vegetables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserDeta__CB9A1CFF63090B60");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("User_Password");
        });

        modelBuilder.Entity<Vegetable>(entity =>
        {
            entity.HasKey(e => e.VegId).HasName("PK__Vegetabl__0AE585807C91E69D");

            entity.Property(e => e.VegId).HasColumnName("Veg_Id");
            entity.Property(e => e.VegName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Veg_Name");

            entity.HasOne(d => d.User).WithMany(p => p.Vegetables)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Vegetable__UserI__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
