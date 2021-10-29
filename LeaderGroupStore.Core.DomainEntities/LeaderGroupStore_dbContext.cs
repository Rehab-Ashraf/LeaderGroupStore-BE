using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LeaderGroupStore.Core.DomainEntities
{
    public partial class LeaderGroupStore_dbContext : DbContext
    {
        public LeaderGroupStore_dbContext()
        {
        }

        public LeaderGroupStore_dbContext(DbContextOptions<LeaderGroupStore_dbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; private set; }
        public  DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LeaderGroupStore_db;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => e.Name, "UQ_Category_Name")
                    .IsUnique();

                

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
                entity.HasMany(c => c.Products);


            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Cost).HasMaxLength(512);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Price).HasMaxLength(128);
                entity.HasIndex(e => e.CategoryId, "UQ__Category__B40CC6CCB11C872E")
                    .IsUnique();
                entity.HasOne(p => p.Category);
            });


            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UQ_User_Email")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(256);

                entity.Property(e => e.LastName).HasMaxLength(256);

                entity.Property(e => e.Password).HasMaxLength(512);

                entity.Property(e => e.PhoneNumber).HasMaxLength(128);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
