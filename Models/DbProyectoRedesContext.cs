using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace U1_evaluacion_sumativa.Models;

public partial class DbProyectoRedesContext : DbContext
{
    public DbProyectoRedesContext()
    {
    }

    public DbProyectoRedesContext(DbContextOptions<DbProyectoRedesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proyectos");

            entity.HasIndex(e => e.UsuarioId, "fk_Proyectos_usuarios_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Descripcion).HasMaxLength(400);
            entity.Property(e => e.Estado)
                .HasComment("0: Finalizado ; 1: Proceso; 2: Cancelado")
                .HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Tipo).HasColumnType("int(11)");
            entity.Property(e => e.UsuarioId).HasColumnType("int(11)");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Proyectos_usuarios");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Estado)
                .HasComment("0: Desactivado ; 1: Activado")
                .HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Rol)
                .HasComment("0: Administador ; 1: Trabajador")
                .HasColumnType("int(11)");
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
