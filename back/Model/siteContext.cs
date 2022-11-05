using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace back.Model
{
    public partial class siteContext : DbContext
    {
        public siteContext()
        {
        }

        public siteContext(DbContextOptions<siteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cargo> Cargos { get; set; } = null!;
        public virtual DbSet<PostAtividade> PostAtividades { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=SNCCH01LABF106\\SQLEXPRESS;Initial Catalog=site;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.ToTable("Cargo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cargo1)
                    .IsUnicode(false)
                    .HasColumnName("Cargo");

                entity.HasOne(d => d.CargoNavigation)
                    .WithMany(p => p.Cargos)
                    .HasForeignKey(d => d.CargoId)
                    .HasConstraintName("FK__Cargo__CargoId__2C3393D0");
            });

            modelBuilder.Entity<PostAtividade>(entity =>
            {
                entity.ToTable("PostAtividade");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Conteudo).IsUnicode(false);

                entity.Property(e => e.Momento).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostAtividades)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__PostAtivi__UserI__29572725");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.ToTable("Token");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Value).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Token__UserID__267ABA7A");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Confsenha).IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Matricula)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Senha).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
