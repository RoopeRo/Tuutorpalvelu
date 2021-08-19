using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication1.Models
{
    public partial class TutorpalveluDBContext : DbContext
    {
        public TutorpalveluDBContext()
        {
        }

        public TutorpalveluDBContext(DbContextOptions<TutorpalveluDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Palvelu> Palvelus { get; set; }
        public virtual DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Palvelu>(entity =>
            {
                entity.ToTable("Palvelu");

                entity.Property(e => e.PalveluId).HasColumnName("PalveluID");

                entity.Property(e => e.Hinta).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Nimi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pvm).HasColumnType("datetime");

                entity.Property(e => e.Ryhmä)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sijainti)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TutorId).HasColumnName("TutorID");

                entity.Property(e => e.Tyyppi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Kuvaus)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.Palvelus)
                    .HasForeignKey(d => d.TutorId)
                    .HasConstraintName("FK__Palvelu__TutorID__267ABA7A");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Etunimi)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Osoite)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Postinumero)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Postitoimipaikka)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PuhNro)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sukunimi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
