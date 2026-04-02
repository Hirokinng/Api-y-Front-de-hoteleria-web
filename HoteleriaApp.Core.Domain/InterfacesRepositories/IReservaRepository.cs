using HoteleriaApp.Core.Domain.Entities;

public interface IReservaRepository : IGenericRepository<Reserva>
{
    Task<IReadOnlyList<Reserva>> GetAllWithDetailsAsync();
    Task<Reserva?> GetByIdWithDetailsAsync(Guid id);
    Task<IReadOnlyList<Habitacion>> GetHabitacionesDisponiblesAsync(
        Guid idCategoria, DateOnly fechaEntrada, DateOnly fechaSalida, byte numHuespedes);
    Task<bool> ExisteSolapamientoAsync(Guid idHabitacion, DateOnly fechaEntrada, DateOnly fechaSalida, Guid? excludeReservaId = null);
}
