using HoteleriaApp.Core.Domain.Entities;
using System.Threading.Tasks;
using HoteleriaApp.Core.Application.DTOs.Reservas;

namespace HoteleriaApp.Core.Application.Interfaces
{
    public interface IReservaService
    {
        Task<IReadOnlyList<ReservaDto>> GetAllAsync();
        Task<ReservaDto?> GetByIdAsync(int id);
        Task<IReadOnlyList<HabitacionDisponibleDto>> BuscarDisponibilidadAsync(
            int idCategoria, DateOnly fechaEntrada, DateOnly fechaSalida, byte numHuespedes);
        Task<(bool Ok, string? Error, ReservaDto? Reserva)> CrearAsync(CrearReservaDto dto);
        Task<(bool Ok, string? Error)> EditarAsync(EditarReservaDto dto);
        Task<(bool Ok, string? Error)> CancelarAsync(int id);
    }
}
