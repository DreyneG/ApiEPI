using System;
using System.Collections.Generic;
using API_SAFEGUARD.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API_SAFEGUARD.Context;

public partial class AppDbContext : IdentityDbContext<ApplicationUser>
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Colaborador> Colaboradors { get; set; }

    public virtual DbSet<Entrega> Entregas { get; set; }

    public virtual DbSet<Epi> Epis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=safeguard;UserId=postgres;Password=senai901;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Colaborador>(entity =>
        {
            entity.HasKey(e => e.IdColaborador).HasName("epi_pkey");

            entity.ToTable("colaborador");

            entity.HasIndex(e => e.Cpf, "epi_cpf_key").IsUnique();

            entity.HasIndex(e => e.Telefone, "epi_telefone_key").IsUnique();

            entity.HasIndex(e => e.Cpf, "idx_cpf");

            entity.HasIndex(e => e.IdColaborador, "idx_id_colaborador");

            entity.Property(e => e.IdColaborador)
                .HasDefaultValueSql("nextval('epi_id_colaborador_seq'::regclass)")
                .HasColumnName("id_colaborador");
            entity.Property(e => e.Cpf).HasColumnName("cpf");
            entity.Property(e => e.Ctps).HasColumnName("ctps");
            entity.Property(e => e.DataDeAdmisao).HasColumnName("data_de_admisao");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.NomeColaborador)
                .HasMaxLength(100)
                .HasColumnName("nome_colaborador");
            entity.Property(e => e.Telefone).HasColumnName("telefone");
            entity.Property(e => e.TipoFuncionario)
                .HasMaxLength(1)
                .HasColumnName("tipo_funcionario");
        });

        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => e.IdEntrega).HasName("entrega_pkey");

            entity.ToTable("entrega");

            entity.HasIndex(e => e.IdColaborador, "entrega_id_colaborador_key").IsUnique();

            entity.HasIndex(e => e.IdEpi, "entrega_id_epi_key").IsUnique();

            entity.HasIndex(e => e.IdEntrega, "idx_id_entrega");

            entity.Property(e => e.IdEntrega).HasColumnName("id_entrega");
            entity.Property(e => e.DataDeEntrega).HasColumnName("data_de_entrega");
            entity.Property(e => e.IdColaborador).HasColumnName("id_colaborador");
            entity.Property(e => e.IdEpi).HasColumnName("id_epi");
            entity.Property(e => e.Validade).HasColumnName("validade");

            entity.HasOne(d => d.IdColaboradorNavigation).WithOne(p => p.Entrega)
                .HasForeignKey<Entrega>(d => d.IdColaborador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entrega_id_colaborador_fkey");

            entity.HasOne(d => d.IdEpiNavigation).WithOne(p => p.Entrega)
                .HasForeignKey<Entrega>(d => d.IdEpi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entrega_id_epi_fkey");
        });

        modelBuilder.Entity<Epi>(entity =>
        {
            entity.HasKey(e => e.IdEpi).HasName("epi_pkey1");

            entity.ToTable("epi");

            entity.HasIndex(e => e.IdEpi, "idx_epi");

            entity.Property(e => e.IdEpi).HasColumnName("id_epi");
            entity.Property(e => e.Instrucoes)
                .HasMaxLength(3000)
                .HasColumnName("instrucoes");
            entity.Property(e => e.NomeEpi)
                .HasMaxLength(50)
                .HasColumnName("nome_epi");
            entity.Property(e => e.Qtd).HasColumnName("qtd");
        });

        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
