using Microsoft.EntityFrameworkCore;
using System;

public class HabitacionRepository : GenericRepository<Habitacion>, IHabitacionRepository
{
    public HabitacionRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyList<Habitacion>> GetDisponiblesAsync(
        int idCategoria, DateOnly fechaEntrada, DateOnly fechaSalida, byte numHuespedes)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(h => h.Categoria)
            .Include(h => h.Piso)
            .Where(h => h.IdCategoria == idCategoria
                     && h.Estado == EstadoHabitacion.Disponible
                     && h.Categoria.CapacidadMax >= numHuespedes
                     && !h.DetallesReserva.Any(d =>
                            d.Reserva.Estado != EstadoReserva.Cancelada &&
                            d.Reserva.Estado != EstadoReserva.CheckOut &&
                            d.Reserva.FechaEntrada < fechaSalida &&
                            d.Reserva.FechaSalida > fechaEntrada))
            .ToListAsync();
    }
}
