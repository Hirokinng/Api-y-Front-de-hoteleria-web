using HoteleriaApp.Core.Application.Interfaces;
using HoteleriaApp.Core.Domain.Entities;
using HoteleriaApp.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HoteleriaApp.Core.Application.Services
{
    public class HabitacionesService : IHabitacionesService
    {
        private readonly IApplicationDbContext _context;

        public HabitacionesService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Habitacion>> ObtenerInventarioAsync()
        {
            return await _context.Habitaciones
                .Include(h => h.TipoHabitacion)
                .Include(h => h.Amenities)
                    .ThenInclude(ha => ha.Amenity)
                .Include(h => h.Reservas)
                    .ThenInclude(rh => rh.Reserva)
                .Include(h => h.Bloqueos)
                .ToListAsync();
        }

        public async Task<List<Habitacion>> BuscarDisponiblesAsync(
            DateOnly fechaInicio,
            DateOnly fechaFin,
            Guid? tipoHabitacionId = null,
            int? capacidadMinima = null,
            List<Guid>? amenitiesIds = null)  // Cambiado de List<int> a List<Guid>
        {
            var query = _context.Habitaciones
                .Include(h => h.TipoHabitacion)
                .Include(h => h.Amenities)
                    .ThenInclude(ha => ha.Amenity)
                .Include(h => h.Reservas)
                    .ThenInclude(rh => rh.Reserva)
                .Include(h => h.Bloqueos)
                .AsQueryable();

            if (tipoHabitacionId.HasValue)
            {
                query = query.Where(h => h.TipoHabitacionId == tipoHabitacionId.Value);
            }

            if (capacidadMinima.HasValue)
            {
                query = query.Where(h => h.Capacidad >= capacidadMinima.Value);
            }

            if (amenitiesIds is not null && amenitiesIds.Count > 0)
            {
                query = query.Where(h =>
                    amenitiesIds.All(aid => h.Amenities.Any(ha => ha.AmenityId == aid)));
            }

            query = query.Where(h =>
                !h.Reservas.Any(rh =>
                    rh.Reserva.FechaEntrada < fechaFin &&
                    fechaInicio < rh.Reserva.FechaSalida) &&
                !h.Bloqueos.Any(b =>
                    b.FechaInicio < fechaFin &&
                    fechaInicio < b.FechaFin));

            return await query.ToListAsync();
        }

        public async Task<bool> AsignarHabitacionAReservaAsync(Guid reservaId, Guid habitacionId)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Habitaciones)
                    .ThenInclude(rh => rh.Habitacion)
                .FirstOrDefaultAsync(r => r.Id == reservaId);  // Comparación con Guid

            if (reserva is null) return false;

            var habitacion = await _context.Habitaciones
                .Include(h => h.Reservas)
                    .ThenInclude(rh => rh.Reserva)
                .Include(h => h.Bloqueos)
                .FirstOrDefaultAsync(h => h.Id == habitacionId);  // Comparación con Guid

            if (habitacion is null) return false;

            if (habitacion.Capacidad < reserva.NumeroHuespedes) return false;

            var fechaInicio = reserva.FechaEntrada;
            var fechaFin = reserva.FechaSalida;

            if (habitacion.Reservas.Any(rh =>
                rh.Reserva.FechaEntrada < fechaFin &&
                fechaInicio < rh.Reserva.FechaSalida)) return false;

            if (habitacion.Bloqueos.Any(b =>
                b.FechaInicio < fechaFin &&
                fechaInicio < b.FechaFin)) return false;

            var asignacion = new ReservaHabitacion
            {
                ReservaId = reserva.Id,
                HabitacionId = habitacion.Id,
                EsPrincipal = !reserva.Habitaciones.Any()
            };

            reserva.Habitaciones.Add(asignacion);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BloqueoHabitacion?> CrearBloqueoHabitacionAsync(
            Guid habitacionId,
            DateOnly fechaInicio,
            DateOnly fechaFin,
            string motivo,
            TipoBloqueoHabitacion tipo)
        {
            if (fechaFin <= fechaInicio) return null;

            var habitacion = await _context.Habitaciones
                .Include(h => h.Bloqueos)
                .Include(h => h.Reservas)
                    .ThenInclude(rh => rh.Reserva)
                .FirstOrDefaultAsync(h => h.Id == habitacionId); // Comparación con Guid

            if (habitacion is null) return null;

            if (habitacion.Reservas.Any(rh =>
                rh.Reserva.FechaEntrada < fechaFin &&
                fechaInicio < rh.Reserva.FechaSalida)) return null;

            if (habitacion.Bloqueos.Any(b =>
                b.FechaInicio < fechaFin &&
                fechaInicio < b.FechaFin)) return null;

            var bloqueo = new BloqueoHabitacion
            {
                HabitacionId = habitacion.Id,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                Motivo = motivo,
                TipoBloqueo = tipo
            };

            habitacion.Bloqueos.Add(bloqueo);

            await _context.SaveChangesAsync();

            return bloqueo;
        }
    }
}