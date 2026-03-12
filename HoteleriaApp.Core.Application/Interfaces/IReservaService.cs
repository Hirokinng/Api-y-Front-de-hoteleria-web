using System;

public interface IReservaService
{
    Task<ReservaDto?> GetByIdAsync(int id);
    Task<ReservaDto?> GetByNumeroReservaAsync(string numeroReserva);
    Task<IReadOnlyList<ReservaDto>> GetByClienteAsync(int idCliente);
    Task<ReservaDto> CrearAsync(CrearReservaDto dto);
    Task<ReservaDto> ActualizarAsync(ActualizarReservaDto dto);
    Task CancelarAsync(int id, int? idUsuario, string? observacion);
}
