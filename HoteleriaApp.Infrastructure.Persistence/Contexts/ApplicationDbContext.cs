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

            // 1. ID fijo (obligatorio para que EF Core no se confunda)
            var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            // 2. Sembrar el usuario usando exactamente tus propiedades
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = adminId, // Viene de BaseEntity
                    Nombre = "Administrador del Sistema",
                    Email = "admin@hotel.com",

                    // OJO AQUÍ: Como tu propiedad se llama PasswordHash, idealmente debería ser una contraseña encriptada. 
                    // Si para pruebas su sistema acepta texto plano temporalmente, déjalo así. 
                    // Si usan BCrypt u otro, tendrás que poner el texto ya encriptado aquí.
                    PasswordHash = "$2a$12$7VSbFVWWrwtu3w5UpUEkmuNnlrhxPWM1JIXslLB./qvp.1wAJh39K",
                
                    Rol = RolUsuario.Administrador,

                    Activo = true,

                    // TRUCO PRO: Nunca uses DateTime.UtcNow en un Seed. 
                    // Si lo haces, Entity Framework te pedirá hacer una migración nueva cada segundo que pase. Usa una fecha fija.
                    FechaCreacion = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }

                );
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly
            );
        }
    }
}

