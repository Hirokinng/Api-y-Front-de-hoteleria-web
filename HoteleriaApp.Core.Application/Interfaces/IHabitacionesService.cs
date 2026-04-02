using HoteleriaApp.Core.Domain.Entities;

namespace HoteleriaApp.Core.Application.Interfaces
{
    public interface IHabitacionesService
    {
        Task<List<Habitacion>> ObtenerInventarioAsync();

        Task<List<Habitacion>> BuscarDisponiblesAsync(
            DateOnly fechaInicio,
            DateOnly fechaFin,
            Guid? tipoHabitacionId = null,
            int? capacidadMinima = null,
            List<Guid>? amenitiesIds = null);

        Task<bool> AsignarHabitacionAReservaAsync(Guid reservaId, Guid habitacionId);

        Task<BloqueoHabitacion?> CrearBloqueoHabitacionAsync(
            Guid habitacionId,
            DateOnly fechaInicio,
            DateOnly fechaFin,
            string motivo,
            Core.Domain.Enums.TipoBloqueoHabitacion tipo);

        Task<Habitacion?> ObtenerPorIdAsync(Guid id);
        Task<Habitacion> CrearHabitacionAsync(string numero, int piso, Guid tipoHabitacionId, int capacidad, Guid idCategoria);
        Task<bool> EliminarHabitacionAsync(Guid id);
    }
}
