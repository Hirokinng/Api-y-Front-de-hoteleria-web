using HoteleriaApp.Core.Domain.Entities;
using HoteleriaApp.Infrastructure.Persistence.Contexts;
using HoteleriaApp.Core.Domain.Enums;
using HoteleriaApp.Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace HoteleriaApp.Infrastructure.Persistence.Repositories
{
    public class HabitacionRepository : GenericRepository<Habitacion>, IHabitacionRepository
    {
        public HabitacionRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Habitacion>> GetDisponiblesAsync(
            Guid idCategoria, DateOnly fechaEntrada, DateOnly fechaSalida, byte numHuespedes)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(h => h.Categoria)
                .Include(h => h.Piso)
                .Where(h => h.IdCategoria == idCategoria
                         && h.Estado == HabitacionEstado.Disponible
                         && h.Categoria.Capacidad >= numHuespedes
                         && !h.DetallesReserva.Any(d =>
                                d.Reserva.Estado != EstadoReserva.Cancelada &&
                                d.Reserva.Estado != EstadoReserva.CheckOut &&
                                d.Reserva.FechaEntrada < fechaSalida &&
                                d.Reserva.FechaSalida > fechaEntrada))
                .ToListAsync();
        }
    }

}