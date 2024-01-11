using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AcmeForms.Models
{
    public partial class acmeformsContext : DbContext
    {
        public acmeformsContext()
        {
        }

        public acmeformsContext(DbContextOptions<acmeformsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Field> Fields { get; set; } = null!;
        public virtual DbSet<FieldsType> FieldsTypes { get; set; } = null!;
        public virtual DbSet<Form> Forms { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Field>(entity =>
            {
                entity.HasIndex(e => e.FormId, "IX_Relationship2");

                entity.HasIndex(e => e.TypeId, "IX_Relationship4");

                entity.Property(e => e.FieldId)
                    .ValueGeneratedNever()
                    .HasColumnName("Field_id");

                entity.Property(e => e.FormId).HasColumnName("Form_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("Type_id");

                entity.HasOne(d => d.oForm)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.FormId)
                    .HasConstraintName("Relationship2");

                entity.HasOne(d => d.oType)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("Relationship4");
            });

            modelBuilder.Entity<FieldsType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("Fields_Type");

                entity.Property(e => e.TypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("Type_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Form>(entity =>
            {
                entity.ToTable("Form");

                entity.HasIndex(e => e.UserId, "IX_Relationship3");

                entity.Property(e => e.FormId)
                    .ValueGeneratedNever()
                    .HasColumnName("Form_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.HasOne(d => d.oUser)
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Relationship3");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("User_id");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Full_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.User1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
