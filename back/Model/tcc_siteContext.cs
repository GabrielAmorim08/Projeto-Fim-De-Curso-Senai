using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace back.Model
{
    public partial class tcc_siteContext : DbContext
    {
        public tcc_siteContext()
        {
        }

        public tcc_siteContext(DbContextOptions<tcc_siteContext> options)
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

                optionsBuilder.UseSqlServer("Data Source=CLAUDIAAMORIM\\SQLEXPRESS;Initial Catalog=tcc_site;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.ToTable("cargos");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CargoId).HasColumnName("CargoID");

                entity.HasOne(d => d.CargoNavigation)
                    .WithMany(p => p.Cargos)
                    .HasForeignKey(d => d.CargoId)
                    .HasConstraintName("FK__cargos__CargoID__2C3393D0");
            });

            modelBuilder.Entity<PostAtividade>(entity =>
            {
                entity.ToTable("PostAtividade");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Content).IsUnicode(false);

                entity.Property(e => e.Moment).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostAtividades)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__PostAtivi__UserI__29572725");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.ToTable("Token");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Valor)
                    .IsUnicode(false)
                    .HasColumnName("valor");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Token__UserId__267ABA7A");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Confsenha)
                    .IsUnicode(false)
                    .HasColumnName("confsenha");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Matricula)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("matricula");

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.Senha)
                    .IsUnicode(false)
                    .HasColumnName("senha");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
