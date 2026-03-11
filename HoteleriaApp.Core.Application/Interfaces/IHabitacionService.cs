using System;

public interface IHabitacionService
{
    Task<IReadOnlyList<HabitacionDisponibleDto>> GetDisponiblesAsync(
        int idCategoria, DateOnly fechaEntrada, DateOnly fechaSalida, byte numHuespedes);
}
