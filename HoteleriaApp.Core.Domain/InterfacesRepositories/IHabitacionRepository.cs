using System;

public interface IHabitacionRepository : IGenericRepository<Habitacion>
{
    Task<IReadOnlyList<Habitacion>> GetDisponiblesAsync(
        int idCategoria, DateOnly fechaEntrada, DateOnly fechaSalida, byte numHuespedes);
}
