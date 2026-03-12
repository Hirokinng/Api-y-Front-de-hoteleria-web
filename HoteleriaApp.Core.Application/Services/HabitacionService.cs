using AutoMapper;
using System;

public class HabitacionService : IHabitacionService
{
    private readonly IHabitacionRepository _habitacionRepository;
    private readonly IMapper _mapper;

    public HabitacionService(IHabitacionRepository habitacionRepository, IMapper mapper)
    {
        _habitacionRepository = habitacionRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<HabitacionDisponibleDto>> GetDisponiblesAsync(
        int idCategoria, DateOnly fechaEntrada, DateOnly fechaSalida, byte numHuespedes)
    {
        var habitaciones = await _habitacionRepository
            .GetDisponiblesAsync(idCategoria, fechaEntrada, fechaSalida, numHuespedes);

        return _mapper.Map<IReadOnlyList<HabitacionDisponibleDto>>(habitaciones);
    }
}