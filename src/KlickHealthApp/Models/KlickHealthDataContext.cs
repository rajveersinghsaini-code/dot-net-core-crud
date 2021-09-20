using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace KlickHealthApp.Models
{
    public partial class KlickHealthDataContext : DbContext
    {
        public KlickHealthDataContext()
        {
        }

        public KlickHealthDataContext(DbContextOptions<KlickHealthDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MedicationDetail> MedicationDetails { get; set; }
        public virtual DbSet<MedicineInfo> MedicineInfos { get; set; }
        public virtual DbSet<ProvinceInfo> ProvinceInfos { get; set; }
        public virtual DbSet<UserInformation> UserInformations { get; set; }
             
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<MedicationDetail>(entity =>
            {
                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.MedicationDetails)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MedicationDerails_MedicineId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MedicationDetails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MedicationDerails_UserId");
            });

            modelBuilder.Entity<MedicineInfo>(entity =>
            {
                entity.ToTable("MedicineInfo");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.MedicineName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProvinceInfo>(entity =>
            {             
                entity.ToTable("ProvinceInfo");

                entity.Property(e => e.ProvinceCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ProvinceName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserInformation>(entity =>
            {
                entity.ToTable("UserInformation");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProvinceCodeNavigation)
                    .WithMany(p => p.UserInformations)
                    .HasForeignKey(d => d.ProvinceCode)
                    .HasConstraintName("FK_UserInformation_ProvinceCode");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
