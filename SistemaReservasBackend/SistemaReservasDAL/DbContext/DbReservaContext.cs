using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaReservasModel;

namespace SistemaReservasDAL;

public partial class DbReservaContext : DbContext
{
    public DbReservaContext()
    {
    }

    public DbReservaContext(DbContextOptions<DbReservaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetalleReserva> DetalleReservas { get; set; }

    public virtual DbSet<Espacio> Espacios { get; set; }

    public virtual DbSet<FormaPago> FormaPagos { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRol> MenuRols { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetalleReserva>(entity =>
        {
            entity.HasKey(e => e.IdDetalleReserva).HasName("PK__DetalleR__74EEC7D1251F45F2");

            entity.ToTable("DetalleReserva");

            entity.Property(e => e.IdDetalleReserva).HasColumnName("idDetalleReserva");
            entity.Property(e => e.CantHoras).HasColumnName("cantHoras");
            entity.Property(e => e.IdEspacio).HasColumnName("idEspacio");
            entity.Property(e => e.IdReserva).HasColumnName("idReserva");
            entity.Property(e => e.PrecioPorHora)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioPorHora");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdEspacioNavigation).WithMany(p => p.DetalleReservas)
                .HasForeignKey(d => d.IdEspacio)
                .HasConstraintName("FK__DetalleRe__idEsp__403A8C7D");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.DetalleReservas)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("FK__DetalleRe__idRes__3F466844");
        });

        modelBuilder.Entity<Espacio>(entity =>
        {
            entity.HasKey(e => e.IdEspacio).HasName("PK__Espacio__87025B445D400B27");

            entity.ToTable("Espacio");

            entity.Property(e => e.IdEspacio).HasColumnName("idEspacio");
            entity.Property(e => e.Disponibilidad)
                .HasDefaultValueSql("((1))")
                .HasColumnName("disponibilidad");
            entity.Property(e => e.HorasDisponible).HasColumnName("horasDisponible");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioPorHora)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioPorHora");
        });

        modelBuilder.Entity<FormaPago>(entity =>
        {
            entity.HasKey(e => e.IdFormaPago).HasName("PK__FormaPag__952893F60404E701");

            entity.ToTable("FormaPago");

            entity.Property(e => e.IdFormaPago).HasColumnName("idFormaPago");
            entity.Property(e => e.Forma)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("forma");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__C26AF4831171387D");

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("icono");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        modelBuilder.Entity<MenuRol>(entity =>
        {
            entity.HasKey(e => e.IdMenuRol).HasName("PK__MenuRol__9D6D61A4FE9B231A");

            entity.ToTable("MenuRol");

            entity.Property(e => e.IdMenuRol).HasColumnName("idMenuRol");
            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.IdRol).HasColumnName("idRol");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__MenuRol__idMenu__29572725");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__MenuRol__idRol__2A4B4B5E");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reserva__94D104C8BC1D59D9");

            entity.ToTable("Reserva");

            entity.Property(e => e.IdReserva).HasColumnName("idReserva");
            entity.Property(e => e.Dni).HasColumnName("dni");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdFormaPago).HasColumnName("idFormaPago");
            entity.Property(e => e.Tel).HasColumnName("tel");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdFormaPagoNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdFormaPago)
                .HasConstraintName("FK__Reserva__idForma__3B75D760");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F7604334CC5");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A61FCAE237");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.Clave)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreCompleto");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__idRol__2D27B809");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
