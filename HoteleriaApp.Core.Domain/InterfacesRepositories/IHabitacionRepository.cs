using System;
using HoteleriaApp.Core.Domain.Entities;

public interface IHabitacionRepository : IGenericRepository<Habitacion>
{
    Task<IReadOnlyList<Habitacion>> GetDisponiblesAsync(
        Guid idCategoria, DateOnly fechaEntrada, DateOnly fechaSalida, byte numHuespedes);
}
