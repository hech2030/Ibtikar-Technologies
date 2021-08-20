using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=Ecommerce;user=hfakhfakh;pwd=test1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.31-mysql"));
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
