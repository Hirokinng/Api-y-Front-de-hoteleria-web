using HoteleriaApp.Core.Domain.Entities;
using HoteleriaApp.Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HoteleriaApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Piso> Pisos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }

        public DbSet<TipoHabitacion> TiposHabitacion { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<HabitacionAmenity> HabitacionesAmenities { get; set; }
        public DbSet<ReservaHabitacion> ReservasHabitaciones { get; set; }
        public DbSet<BloqueoHabitacion> BloqueosHabitacion { get; set; }

        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Temporada> Temporadas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<DetalleReserva> DetallesReserva { get; set; }
        public DbSet<ReservaServicio> ReservaServicios { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }

        //Agregando DbSets faltantes
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<CategoriaServicio> CategoriasServicio { get; set; }

        // Implementación para GenericRepository
        public override DbSet<T> Set<T>() where T : class => base.Set<T>();

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly
            );
        }
    }
}

