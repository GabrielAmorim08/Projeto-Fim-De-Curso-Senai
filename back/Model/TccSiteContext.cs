using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace back.Model;

public partial class TccSiteContext : DbContext
{
    public TccSiteContext()
    {
    }

    public TccSiteContext(DbContextOptions<TccSiteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<PostAtividade> PostAtividades { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SNCCH01LABF106\\SQLEXPRESS;Initial Catalog=tcc_site;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cargos__3214EC27475DCBC1");

            entity.ToTable("cargos");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CargoId).HasColumnName("CargoID");
            entity.Property(e => e.CargoNome)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.CargoNavigation).WithMany(p => p.Cargos)
                .HasForeignKey(d => d.CargoId)
                .HasConstraintName("FK__cargos__CargoID__2C3393D0");
        });

        modelBuilder.Entity<PostAtividade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostAtiv__3214EC2746B25FFB");

            entity.ToTable("PostAtividade");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).IsUnicode(false);
            entity.Property(e => e.Moment).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.PostAtividades)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__PostAtivi__UserI__29572725");
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Token__3214EC2743698752");

            entity.ToTable("Token");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Valor)
                .IsUnicode(false)
                .HasColumnName("valor");

            entity.HasOne(d => d.User).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Token__UserId__267ABA7A");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC2722DC651A");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Confsenha)
                .IsUnicode(false)
                .HasColumnName("confsenha");
            entity.Property(e => e.DataNascimento).HasColumnType("date");
            entity.Property(e => e.Email)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Endereco)
                .IsUnicode(false)
                .HasColumnName("endereco");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("estado");
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
            entity.Property(e => e.Setor)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefone");
            entity.Property(e => e.Uf)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("UF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
