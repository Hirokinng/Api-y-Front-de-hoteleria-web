using System;

public interface IReservaRepository : IGenericRepository<Reserva>
{
    Task<Reserva?> GetByNumeroReservaAsync(string numeroReserva);
    Task<IReadOnlyList<Reserva>> GetByClienteAsync(int idCliente);
    Task<IReadOnlyList<Reserva>> GetByEstadoAsync(EstadoReserva estado);
    Task<bool> TieneConflictoFechasAsync(int idHabitacion, DateOnly fechaEntrada, DateOnly fechaSalida);
}
