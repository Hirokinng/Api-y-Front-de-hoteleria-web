using HoteleriaApp.Core.Domain.Entities;
using HoteleriaApp.Infrastructure.Persistence.Contexts;
using HoteleriaApp.Core.Domain.Enums;
using HoteleriaApp.Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HoteleriaApp.Infrastructure.Persistence.Repositories
{
    public class ReservaRepository : GenericRepository<Reserva>, IReservaRepository
    {
        public ReservaRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Reserva>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Include(r => r.Cliente)
                .Include(r => r.Category)
                .Include(r => r.DetallesReserva)
                    .ThenInclude(d => d.Habitacion)
                .Include(r => r.ReservaServicios)
                    .ThenInclude(rs => rs.Servicio)
                .OrderByDescending(r => r.FechaCreacion)
                .ToListAsync();
        }

        public async Task<Reserva?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(r => r.Cliente)
                .Include(r => r.Category)
                .Include(r => r.DetallesReserva)
                    .ThenInclude(d => d.Habitacion)
                .Include(r => r.ReservaServicios)
                    .ThenInclude(rs => rs.Servicio)
                .Include(r => r.Pagos)
                .Include(r => r.Historial)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IReadOnlyList<Habitacion>> GetHabitacionesDisponiblesAsync(
            Guid idCategoria, DateOnly fechaEntrada, DateOnly fechaSalida, byte numHuespedes)
        {
            return await _context.Habitaciones
                .AsNoTracking()
                .Include(h => h.TipoHabitacion)
                .Include(h => h.Piso)
                .Where(h =>
                    h.TipoHabitacionId == idCategoria &&
                    h.Estado == HabitacionEstado.Disponible &&
                    h.Capacidad >= numHuespedes &&
                    !h.DetallesReserva.Any(d =>
                        d.Reserva.Estado != EstadoReserva.Cancelada &&
                        d.Reserva.Estado != EstadoReserva.CheckOut &&
                        d.Reserva.FechaEntrada < fechaSalida &&
                        d.Reserva.FechaSalida > fechaEntrada))
                .ToListAsync();
        }

        public async Task<bool> ExisteSolapamientoAsync(
            Guid idHabitacion, DateOnly fechaEntrada, DateOnly fechaSalida, Guid? excludeReservaId = null)
        {
            return await _context.DetallesReserva
                .AnyAsync(d =>
                    d.IdHabitacion == idHabitacion &&
                    (excludeReservaId == null || d.IdReserva != excludeReservaId) &&
                    d.Reserva.Estado != EstadoReserva.Cancelada &&
                    d.Reserva.Estado != EstadoReserva.CheckOut &&
                    d.Reserva.FechaEntrada < fechaSalida &&
                    d.Reserva.FechaSalida > fechaEntrada);
        }
    }

}