using AutoMapper;
using System;

public class ReservaProfile : Profile
{
    public ReservaProfile()
    {
        CreateMap<Reserva, ReservaDto>()
            .ForMember(dest => dest.NombreCliente,
                       opt => opt.MapFrom(src => src.Cliente.Nombre))
            .ForMember(dest => dest.NombreCategoria,
                       opt => opt.MapFrom(src => src.Categoria.Nombre))
            .ForMember(dest => dest.NumeroHabitacion,
                       opt => opt.MapFrom(src => src.DetallesReserva
                           .Select(d => d.Habitacion.NumeroHabitacion)
                           .FirstOrDefault()))
            .ForMember(dest => dest.Servicios,
                       opt => opt.MapFrom(src => src.ReservaServicios
                           .Select(rs => rs.Servicio.Nombre)
                           .ToList()))
            .ForMember(dest => dest.Estado,
                       opt => opt.MapFrom(src => src.Estado.ToString()));

        CreateMap<Habitacion, HabitacionDisponibleDto>()
            .ForMember(dest => dest.NombreCategoria,
                       opt => opt.MapFrom(src => src.Categoria.Nombre))
            .ForMember(dest => dest.CapacidadMax,
                       opt => opt.MapFrom(src => src.Categoria.CapacidadMax))
            .ForMember(dest => dest.NumeroPiso,
                       opt => opt.MapFrom(src => src.Piso.NumeroPiso))
            .ForMember(dest => dest.PrecioNoche,
                       opt => opt.Ignore());

        CreateMap<Servicio, ServicioDto>()
            .ForMember(dest => dest.Precio, opt => opt.Ignore());

        CreateMap<Tarifa, TarifaDto>()
            .ForMember(dest => dest.NombreCategoria,
                       opt => opt.MapFrom(src => src.Categoria.Nombre))
            .ForMember(dest => dest.Temporada,
                       opt => opt.MapFrom(src => src.Temporada != null
                           ? src.Temporada.Nombre
                           : "Base"));
    }
}
