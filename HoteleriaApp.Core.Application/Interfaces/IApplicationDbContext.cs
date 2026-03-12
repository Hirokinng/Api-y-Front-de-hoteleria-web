using HoteleriaApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HoteleriaApp.Core.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Habitacion> Habitaciones { get; }
        DbSet<TipoHabitacion> TiposHabitacion { get; }
        DbSet<Amenity> Amenities { get; }
        DbSet<HabitacionAmenity> HabitacionesAmenities { get; }
        DbSet<Reserva> Reservas { get; }
        DbSet<ReservaHabitacion> ReservasHabitaciones { get; }
        DbSet<BloqueoHabitacion> BloqueosHabitacion { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
