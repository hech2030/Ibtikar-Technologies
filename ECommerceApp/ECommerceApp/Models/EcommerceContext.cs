using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace ECommerceApp.Models
{
    public partial class EcommerceContext : DbContext
    {
        public EcommerceContext()
        {
        }

        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<Orderhi> Orderhis { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            string connectionString = configurationBuilder.Build().GetSection("ConnectionStrings:Default").Value;
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.31-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_bin");

            modelBuilder.Entity<Command>(entity =>
            {
                entity.ToTable("command");

                entity.HasIndex(e => e.OrderId, "OrderId");

                entity.HasIndex(e => e.ProductId, "ProductId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(6)")
                    .HasColumnName("id");

                entity.Property(e => e.OrderId).HasColumnType("int(6)");

                entity.Property(e => e.ProductId).HasColumnType("int(6)");

                entity.Property(e => e.Quantity).HasColumnType("int(10)");
            });

            modelBuilder.Entity<Orderhi>(entity =>
            {
                entity.ToTable("orderhis");

                entity.Property(e => e.Id)
                    .HasColumnType("int(6)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Telephone).HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id).HasColumnType("int(6)");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("category");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasPrecision(10)
                    .HasColumnName("price");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnType("int(10)");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
