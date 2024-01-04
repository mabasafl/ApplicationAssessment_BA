using System;
using System.Collections.Generic;
using Application.Core.Models;
using Application.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Application.Core.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Applications> Applications { get; set; } = null!;
        public DbSet<ApplicationCustomer> ApplicationCustomers { get; set; } = null!;
        public DbSet<BusinessArea> BusinessAreas { get; set; } = null!;
        public DbSet<BusinessAreaRelationship> BusinessAreaRelationships { get; set; } = null!;
        public DbSet<BusinessAreaType> BusinessAreaTypes { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Users> Users { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=.; Database=InductionDb; Trusted_Connection=True; Trust Server Certificate=True;");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Applications>(entity =>
//            {
//                entity.HasIndex(e => e.Name, "UQ__Applicat__737584F6980EE6F5")
//                    .IsUnique();

//                entity.Property(e => e.AlternateId)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.CreatedBy).HasMaxLength(255);

//                entity.Property(e => e.DateCreated)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.DateModified)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

//                entity.Property(e => e.Name)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<ApplicationCustomer>(entity =>
//            {
//                entity.Property(e => e.AlternateId)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.CreatedBy).HasMaxLength(255);

//                entity.Property(e => e.DateCreated)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.DateModified)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

//                //entity.HasOne(d => d.Application)
//                //    .WithMany(p => p.ApplicationCustomers)
//                //    .HasForeignKey(d => d.ApplicationId)
//                //    .OnDelete(DeleteBehavior.ClientSetNull)
//                //    .HasConstraintName("FK__Applicati__Appli__619B8048");

//                entity.HasOne(d => d.BusinessArea)
//                    .WithMany(p => p.ApplicationCustomers)
//                    .HasForeignKey(d => d.BusinessAreaId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__Applicati__Busin__628FA481");

//                entity.HasOne(d => d.Customer)
//                    .WithMany(p => p.ApplicationCustomers)
//                    .HasForeignKey(d => d.CustomerId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__Applicati__Custo__60A75C0F");
//            });

//            modelBuilder.Entity<BusinessArea>(entity =>
//            {
//                entity.Property(e => e.AlternateId)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.BusinessAreaTypeId).HasColumnName("BusinessAreaTypeID");

//                entity.Property(e => e.CreatedBy).HasMaxLength(255);

//                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

//                entity.Property(e => e.DateCreated)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.DateModified)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

//                entity.Property(e => e.Name)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.HasOne(d => d.BusinessAreaType)
//                    .WithMany(p => p.BusinessAreas)
//                    .HasForeignKey(d => d.BusinessAreaTypeId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__BusinessA__Modif__47DBAE45");

//                entity.HasOne(d => d.Customer)
//                    .WithMany(p => p.BusinessAreas)
//                    .HasForeignKey(d => d.CustomerId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__BusinessA__Custo__48CFD27E");
//            });

//            modelBuilder.Entity<BusinessAreaRelationship>(entity =>
//            {
//                entity.Property(e => e.AlternateId)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.CreatedBy).HasMaxLength(255);

//                entity.Property(e => e.DateCreated)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.DateModified)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

//                entity.HasOne(d => d.BusinessArea)
//                    .WithMany(p => p.BusinessAreaRelationshipBusinessAreas)
//                    .HasForeignKey(d => d.BusinessAreaId)
//                    .HasConstraintName("FK__BusinessA__Busin__5441852A");

//                entity.HasOne(d => d.Customer)
//                    .WithMany(p => p.BusinessAreaRelationships)
//                    .HasForeignKey(d => d.CustomerId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__BusinessA__Modif__534D60F1");

//                entity.HasOne(d => d.FilteredBusinessArea)
//                    .WithMany(p => p.BusinessAreaRelationshipFilteredBusinessAreas)
//                    .HasForeignKey(d => d.FilteredBusinessAreaId)
//                    .HasConstraintName("FK__BusinessA__Filte__5535A963");
//            });

//            modelBuilder.Entity<BusinessAreaType>(entity =>
//            {
//                entity.HasIndex(e => e.Name, "UQ__Business__737584F6D7EDE06A")
//                    .IsUnique();

//                entity.Property(e => e.AlternateId)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.CreatedBy).HasMaxLength(255);

//                entity.Property(e => e.DateCreated)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.DateModified)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

//                entity.Property(e => e.Name)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<Customer>(entity =>
//            {
//                entity.HasIndex(e => e.Name, "UQ__Customer__737584F62FB9C553")
//                    .IsUnique();

//                entity.Property(e => e.AlternateId)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.CreatedBy).HasMaxLength(255);

//                entity.Property(e => e.DateCreated)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.DateModified)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

//                entity.Property(e => e.Name)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<Person>(entity =>
//            {
//                entity.HasIndex(e => e.EmailAddress, "UQ__Persons__49A14740E8E4B968")
//                    .IsUnique();

//                entity.Property(e => e.AlternateId)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.CreatedBy).HasMaxLength(255);

//                entity.Property(e => e.DateCreated)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.DateModified)
//                    .HasColumnType("datetime")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.EmailAddress)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.FirstName)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.LastName)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.MobileNumber)
//                    .HasMaxLength(20)
//                    .IsUnicode(false);

//                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

//                entity.HasOne(d => d.Customer)
//                    .WithMany(p => p.People)
//                    .HasForeignKey(d => d.CustomerId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__Persons__Modifie__4E88ABD4");
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
