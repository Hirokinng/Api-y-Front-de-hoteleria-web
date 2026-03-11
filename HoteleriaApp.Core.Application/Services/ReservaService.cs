using AutoMapper;
using System;

public class ReservaService : IReservaService
{
    private readonly IReservaRepository _reservaRepository;
    private readonly IHabitacionRepository _habitacionRepository;
    private readonly IGenericRepository<Tarifa> _tarifaRepository;
    private readonly IGenericRepository<CategoriaServicio> _catServicioRepository;
    private readonly IGenericRepository<ReservaServicio> _reservaServicioRepository;
    private readonly IMapper _mapper;

    public ReservaService(
        IReservaRepository reservaRepository,
        IHabitacionRepository habitacionRepository,
        IGenericRepository<Tarifa> tarifaRepository,
        IGenericRepository<CategoriaServicio> catServicioRepository,
        IGenericRepository<ReservaServicio> reservaServicioRepository,
        IMapper mapper)
    {
        _reservaRepository = reservaRepository;
        _habitacionRepository = habitacionRepository;
        _tarifaRepository = tarifaRepository;
        _catServicioRepository = catServicioRepository;
        _reservaServicioRepository = reservaServicioRepository;
        _mapper = mapper;
    }

    public async Task<ReservaDto?> GetByIdAsync(int id)
    {
        var reserva = await _reservaRepository.GetByIdAsync(id);
        return reserva is null ? null : _mapper.Map<ReservaDto>(reserva);
    }

    public async Task<ReservaDto?> GetByNumeroReservaAsync(string numeroReserva)
    {
        var reserva = await _reservaRepository.GetByNumeroReservaAsync(numeroReserva);
        return reserva is null ? null : _mapper.Map<ReservaDto>(reserva);
    }

    public async Task<IReadOnlyList<ReservaDto>> GetByClienteAsync(int idCliente)
    {
        var reservas = await _reservaRepository.GetByClienteAsync(idCliente);
        return _mapper.Map<IReadOnlyList<ReservaDto>>(reservas);
    }

    public async Task<ReservaDto> CrearAsync(CrearReservaDto dto)
    {
        // Validar que las fechas no sean pasadas
        if (dto.FechaEntrada < DateOnly.FromDateTime(DateTime.Today))
            throw new InvalidOperationException("La fecha de entrada no puede ser anterior a hoy.");

        if (dto.FechaSalida <= dto.FechaEntrada)
            throw new InvalidOperationException("La fecha de salida debe ser posterior a la fecha de entrada.");

        // Validar disponibilidad de la habitación
        var tieneConflicto = await _reservaRepository
            .TieneConflictoFechasAsync(dto.IdHabitacion, dto.FechaEntrada, dto.FechaSalida);

        if (tieneConflicto)
            throw new InvalidOperationException("La habitación no está disponible para las fechas indicadas.");

        // Obtener tarifa vigente (temporada activa tiene prioridad sobre tarifa base)
        var tarifas = await _tarifaRepository
            .FindAsync(t => t.IdCategoria == dto.IdCategoria && t.Activo);

        var tarifa = tarifas
            .Where(t => t.IdTemporada == null ||
                       (t.Temporada!.Activo &&
                        dto.FechaEntrada >= t.Temporada.FechaInicio &&
                        dto.FechaEntrada <= t.Temporada.FechaFin))
            .OrderByDescending(t => t.IdTemporada)
            .FirstOrDefault()
            ?? throw new InvalidOperationException("No existe tarifa configurada para esta categoría.");

        // Calcular totales
        var totalNoches = (short)(dto.FechaSalida.DayNumber - dto.FechaEntrada.DayNumber);
        var subtotalHabitacion = tarifa.PrecioNoche * totalNoches;

        // Obtener precios de servicios seleccionados
        decimal totalServicios = 0;
        var serviciosAgregar = new List<(int IdServicio, decimal Precio)>();

        foreach (var idServicio in dto.IdServicios)
        {
            var catServicio = (await _catServicioRepository
                .FindAsync(cs => cs.IdCategoria == dto.IdCategoria
                              && cs.IdServicio == idServicio
                              && cs.Activo))
                .FirstOrDefault()
                ?? throw new InvalidOperationException($"El servicio {idServicio} no está disponible para esta categoría.");

            totalServicios += catServicio.Precio;
            serviciosAgregar.Add((idServicio, catServicio.Precio));
        }

        // Generar número de reserva
        var numeroReserva = $"RES-{DateTime.UtcNow.Year}-{Guid.NewGuid().ToString()[..6].ToUpper()}";

        var reserva = new Reserva
        {
            NumeroReserva = numeroReserva,
            IdCliente = dto.IdCliente,
            IdUsuario = dto.IdUsuario,
            IdCategoria = dto.IdCategoria,
            FechaEntrada = dto.FechaEntrada,
            FechaSalida = dto.FechaSalida,
            NumeroHuespedes = dto.NumeroHuespedes,
            Estado = EstadoReserva.Confirmada,
            PrecioBaseNoche = tarifa.PrecioNoche,
            TotalNoches = totalNoches,
            SubtotalHabitacion = subtotalHabitacion,
            TotalServicios = totalServicios,
            Total = subtotalHabitacion + totalServicios,
            DetallesReserva =
            [
                new DetalleReserva { IdHabitacion = dto.IdHabitacion }
            ],
            ReservaServicios = serviciosAgregar
                .Select(s => new ReservaServicio
                {
                    IdServicio = s.IdServicio,
                    PrecioAplicado = s.Precio
                }).ToList(),
            Historiales =
            [
                new HistorialEstadoReserva
                {
                    EstadoAnterior = null,
                    EstadoNuevo    = EstadoReserva.Confirmada,
                    IdUsuario      = dto.IdUsuario
                }
            ]
        };

        await _reservaRepository.AddAsync(reserva);
        await _reservaRepository.SaveChangesAsync();

        return _mapper.Map<ReservaDto>(reserva);
    }

    public async Task<ReservaDto> ActualizarAsync(ActualizarReservaDto dto)
    {
        var reserva = await _reservaRepository.GetByIdAsync(dto.Id)
            ?? throw new KeyNotFoundException($"Reserva {dto.Id} no encontrada.");

        if (reserva.Estado == EstadoReserva.Cancelada || reserva.Estado == EstadoReserva.CheckOut)
            throw new InvalidOperationException("No se puede modificar una reserva cancelada o finalizada.");

        if (dto.FechaSalida <= dto.FechaEntrada)
            throw new InvalidOperationException("La fecha de salida debe ser posterior a la fecha de entrada.");

        // Recalcular totales si cambiaron las fechas
        var totalNoches = (short)(dto.FechaSalida.DayNumber - dto.FechaEntrada.DayNumber);
        var subtotalHabitacion = reserva.PrecioBaseNoche * totalNoches;

        // Recalcular servicios
        decimal totalServicios = 0;
        var serviciosNuevos = new List<ReservaServicio>();

        foreach (var idServicio in dto.IdServicios)
        {
            var catServicio = (await _catServicioRepository
                .FindAsync(cs => cs.IdCategoria == reserva.IdCategoria
                              && cs.IdServicio == idServicio
                              && cs.Activo))
                .FirstOrDefault()
                ?? throw new InvalidOperationException($"El servicio {idServicio} no está disponible para esta categoría.");

            totalServicios += catServicio.Precio;
            serviciosNuevos.Add(new ReservaServicio
            {
                IdReserva = reserva.Id,
                IdServicio = idServicio,
                PrecioAplicado = catServicio.Precio
            });
        }

        reserva.FechaEntrada = dto.FechaEntrada;
        reserva.FechaSalida = dto.FechaSalida;
        reserva.NumeroHuespedes = dto.NumeroHuespedes;
        reserva.TotalNoches = totalNoches;
        reserva.SubtotalHabitacion = subtotalHabitacion;
        reserva.TotalServicios = totalServicios;
        reserva.Total = subtotalHabitacion + totalServicios;
        reserva.ReservaServicios = serviciosNuevos;

        _reservaRepository.Update(reserva);
        await _reservaRepository.SaveChangesAsync();

        return _mapper.Map<ReservaDto>(reserva);
    }

    public async Task CancelarAsync(int id, int? idUsuario, string? observacion)
    {
        var reserva = await _reservaRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Reserva {id} no encontrada.");

        if (reserva.Estado == EstadoReserva.Cancelada)
            throw new InvalidOperationException("La reserva ya está cancelada.");

        if (reserva.Estado == EstadoReserva.CheckOut)
            throw new InvalidOperationException("No se puede cancelar una reserva finalizada.");

        var estadoAnterior = reserva.Estado;
        reserva.Estado = EstadoReserva.Cancelada;
        reserva.FechaCancelacion = DateTime.UtcNow;

        reserva.Historiales.Add(new HistorialEstadoReserva
        {
            EstadoAnterior = estadoAnterior,
            EstadoNuevo = EstadoReserva.Cancelada,
            IdUsuario = idUsuario,
            Observacion = observacion
        });

        _reservaRepository.Update(reserva);
        await _reservaRepository.SaveChangesAsync();
    }
}
