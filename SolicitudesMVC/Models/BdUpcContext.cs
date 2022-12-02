using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SolicitudesMVC.Models;

public partial class BdUpcContext : DbContext
{
    public BdUpcContext()
    {
    }

    public BdUpcContext(DbContextOptions<BdUpcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<DetalleSolicitud> DetalleSolicitudes { get; set; }

    public virtual DbSet<Solicitud> Solicitudes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server = localhost; Database = bd_upc; Integrated Security = True; Encrypt = False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.IdAlumno);

            entity.ToTable("Alumno");

            entity.Property(e => e.IdAlumno).ValueGeneratedNever();
            entity.Property(e => e.Apellidos)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(1000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK_Cursos");

            entity.ToTable("Curso");

            entity.Property(e => e.IdCurso).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(1000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetalleSolicitud>(entity =>
        {
            entity.HasKey(e => e.IdDetalleSol);

            entity.ToTable("DetalleSolicitud");

            entity.Property(e => e.IdDetalleSol).ValueGeneratedNever();
            entity.Property(e => e.Aula)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Observación)
                .HasMaxLength(4000)
                .IsUnicode(false);
            entity.Property(e => e.Profesor)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Sede)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.DetalleSolicituds)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleSolicitud_Curso");

            entity.HasOne(d => d.IdSolicitudNavigation).WithMany(p => p.DetalleSolicitudes)
                .HasForeignKey(d => d.IdSolicitud)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleSolicitud_Solicitud");
        });

        modelBuilder.Entity<Solicitud>(entity =>
        {
            entity.HasKey(e => e.IdSolicitud);

            entity.ToTable("Solicitud");

            entity.Property(e => e.IdSolicitud).ValueGeneratedNever();
            entity.Property(e => e.Carrera)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.CodRegistrante)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.FechaSolicitud).HasColumnType("datetime");
            entity.Property(e => e.Periodo)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.Solicitudes)
                .HasForeignKey(d => d.IdAlumno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Solicitud_Alumno");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
