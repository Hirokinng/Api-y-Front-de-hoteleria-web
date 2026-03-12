using HoteleriaApp.Core.Domain.Entities;

namespace HoteleriaApp.Core.Application.Interfaces
{
    public interface IHabitacionesService
    {
        Task<List<Habitacion>> ObtenerInventarioAsync();

        Task<List<Habitacion>> BuscarDisponiblesAsync(
            DateOnly fechaInicio,
            DateOnly fechaFin,
            int? tipoHabitacionId = null,
            int? capacidadMinima = null,
            List<int>? amenitiesIds = null);

        Task<bool> AsignarHabitacionAReservaAsync(int reservaId, int habitacionId);

        Task<BloqueoHabitacion?> CrearBloqueoHabitacionAsync(
            int habitacionId,
            DateOnly fechaInicio,
            DateOnly fechaFin,
            string motivo,
            Core.Domain.Enums.TipoBloqueoHabitacion tipo);
    }
}
