using Microsoft.EntityFrameworkCore;
using System;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Piso> Pisos => Set<Piso>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Temporada> Temporadas => Set<Temporada>();
    public DbSet<Servicio> Servicios => Set<Servicio>();
    public DbSet<CategoriaServicio> CategoriaServicios => Set<CategoriaServicio>();
    public DbSet<Tarifa> Tarifas => Set<Tarifa>();
    public DbSet<Habitacion> Habitaciones => Set<Habitacion>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<DetalleReserva> DetallesReserva => Set<DetalleReserva>();
    public DbSet<ReservaServicio> ReservaServicios => Set<ReservaServicio>();
    public DbSet<Pago> Pagos => Set<Pago>();
    public DbSet<HistorialEstadoReserva> HistorialesEstadoReserva => Set<HistorialEstadoReserva>();
    public DbSet<Auditoria> Auditorias => Set<Auditoria>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
