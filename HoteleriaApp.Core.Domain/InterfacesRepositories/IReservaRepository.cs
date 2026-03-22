using HoteleriaApp.Core.Domain.Entities;

public interface IReservaRepository : IGenericRepository<Reserva>
{
    Task<IReadOnlyList<Reserva>> GetAllWithDetailsAsync();
    Task<Reserva?> GetByIdWithDetailsAsync(int id);
    Task<IReadOnlyList<Habitacion>> GetHabitacionesDisponiblesAsync(
        int idCategoria, DateOnly fechaEntrada, DateOnly fechaSalida, byte numHuespedes);
    Task<bool> ExisteSolapamientoAsync(int idHabitacion, DateOnly fechaEntrada, DateOnly fechaSalida, int? excludeReservaId = null);
}
