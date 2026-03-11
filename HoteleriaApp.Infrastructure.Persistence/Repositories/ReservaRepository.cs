using Microsoft.EntityFrameworkCore;
using System;

public class ReservaRepository : GenericRepository<Reserva>, IReservaRepository
{
    public ReservaRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Reserva?> GetByNumeroReservaAsync(string numeroReserva) =>
        await _dbSet
            .AsNoTracking()
            .Include(r => r.Cliente)
            .Include(r => r.Categoria)
            .Include(r => r.DetallesReserva).ThenInclude(d => d.Habitacion)
            .Include(r => r.ReservaServicios).ThenInclude(rs => rs.Servicio)
            .FirstOrDefaultAsync(r => r.NumeroReserva == numeroReserva);

    public async Task<IReadOnlyList<Reserva>> GetByClienteAsync(int idCliente) =>
        await _dbSet
            .AsNoTracking()
            .Where(r => r.IdCliente == idCliente)
            .Include(r => r.Categoria)
            .Include(r => r.ReservaServicios).ThenInclude(rs => rs.Servicio)
            .OrderByDescending(r => r.FechaCreacion)
            .ToListAsync();

    public async Task<IReadOnlyList<Reserva>> GetByEstadoAsync(EstadoReserva estado) =>
        await _dbSet
            .AsNoTracking()
            .Where(r => r.Estado == estado)
            .Include(r => r.Cliente)
            .Include(r => r.Categoria)
            .OrderByDescending(r => r.FechaEntrada)
            .ToListAsync();

    public async Task<bool> TieneConflictoFechasAsync(
        int idHabitacion, DateOnly fechaEntrada, DateOnly fechaSalida) =>
        await _context.DetallesReserva
            .AnyAsync(d => d.IdHabitacion == idHabitacion
                        && d.Reserva.Estado != EstadoReserva.Cancelada
                        && d.Reserva.Estado != EstadoReserva.CheckOut
                        && d.Reserva.FechaEntrada < fechaSalida
                        && d.Reserva.FechaSalida > fechaEntrada);
}