using HoteleriaApp.Core.Application.Interfaces;
using HoteleriaApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HoteleriaApp.Infrastructure.Persistence.Contexts
{
    public class HotelDbContext : DbContext, IApplicationDbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Habitacion> Habitaciones { get; set; } = null!;
        public DbSet<TipoHabitacion> TiposHabitacion { get; set; } = null!;
        public DbSet<Amenity> Amenities { get; set; } = null!;
        public DbSet<HabitacionAmenity> HabitacionesAmenities { get; set; } = null!;
        public DbSet<Reserva> Reservas { get; set; } = null!;
        public DbSet<ReservaHabitacion> ReservasHabitaciones { get; set; } = null!;
        public DbSet<BloqueoHabitacion> BloqueosHabitacion { get; set; } = null!;

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HabitacionAmenity>()
                .HasKey(ha => new { ha.HabitacionId, ha.AmenityId });

            modelBuilder.Entity<HabitacionAmenity>()
                .HasOne(ha => ha.Habitacion)
                .WithMany(h => h.Amenities)
                .HasForeignKey(ha => ha.HabitacionId);

            modelBuilder.Entity<HabitacionAmenity>()
                .HasOne(ha => ha.Amenity)
                .WithMany(a => a.Habitaciones)
                .HasForeignKey(ha => ha.AmenityId);

            modelBuilder.Entity<Habitacion>()
                .HasOne(h => h.TipoHabitacion)
                .WithMany(t => t.Habitaciones)
                .HasForeignKey(h => h.TipoHabitacionId);

            modelBuilder.Entity<ReservaHabitacion>()
                .HasOne(rh => rh.Reserva)
                .WithMany(r => r.Habitaciones)
                .HasForeignKey(rh => rh.ReservaId);

            modelBuilder.Entity<ReservaHabitacion>()
                .HasOne(rh => rh.Habitacion)
                .WithMany(h => h.Reservas)
                .HasForeignKey(rh => rh.HabitacionId);

            modelBuilder.Entity<BloqueoHabitacion>()
                .HasOne(b => b.Habitacion)
                .WithMany(h => h.Bloqueos)
                .HasForeignKey(b => b.HabitacionId);

            // Datos iniciales de ejemplo
            modelBuilder.Entity<TipoHabitacion>().HasData(
                new TipoHabitacion { Id = 1, Nombre = "Single", Descripcion = "Habitación individual", CapacidadBase = 1, PrecioBasePorNoche = 50 },
                new TipoHabitacion { Id = 2, Nombre = "Doble", Descripcion = "Habitación doble", CapacidadBase = 2, PrecioBasePorNoche = 80 },
                new TipoHabitacion { Id = 3, Nombre = "Suite", Descripcion = "Suite familiar", CapacidadBase = 4, PrecioBasePorNoche = 150 }
            );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity { Id = 1, Nombre = "WiFi", Descripcion = "Internet inalámbrico" },
                new Amenity { Id = 2, Nombre = "TV", Descripcion = "Televisión por cable" },
                new Amenity { Id = 3, Nombre = "Minibar", Descripcion = "Bebidas y snacks" },
                new Amenity { Id = 4, Nombre = "Vista al mar", Descripcion = "Vista panorámica" }
            );

            modelBuilder.Entity<Habitacion>().HasData(
                new Habitacion { Id = 1, Numero = "101", Piso = 1, TipoHabitacionId = 1, Capacidad = 1 },
                new Habitacion { Id = 2, Numero = "102", Piso = 1, TipoHabitacionId = 1, Capacidad = 1 },
                new Habitacion { Id = 3, Numero = "201", Piso = 2, TipoHabitacionId = 2, Capacidad = 2 },
                new Habitacion { Id = 4, Numero = "202", Piso = 2, TipoHabitacionId = 2, Capacidad = 2 },
                new Habitacion { Id = 5, Numero = "301", Piso = 3, TipoHabitacionId = 3, Capacidad = 4 }
            );

            modelBuilder.Entity<HabitacionAmenity>().HasData(
                new HabitacionAmenity { HabitacionId = 1, AmenityId = 1 },
                new HabitacionAmenity { HabitacionId = 1, AmenityId = 2 },
                new HabitacionAmenity { HabitacionId = 2, AmenityId = 1 },
                new HabitacionAmenity { HabitacionId = 3, AmenityId = 1 },
                new HabitacionAmenity { HabitacionId = 3, AmenityId = 2 },
                new HabitacionAmenity { HabitacionId = 4, AmenityId = 1 },
                new HabitacionAmenity { HabitacionId = 4, AmenityId = 2 },
                new HabitacionAmenity { HabitacionId = 5, AmenityId = 1 },
                new HabitacionAmenity { HabitacionId = 5, AmenityId = 2 },
                new HabitacionAmenity { HabitacionId = 5, AmenityId = 3 },
                new HabitacionAmenity { HabitacionId = 5, AmenityId = 4 }
            );
        }
    }
}
