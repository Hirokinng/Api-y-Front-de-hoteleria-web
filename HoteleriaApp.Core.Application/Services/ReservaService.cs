using HoteleriaApp.Core.Application.DTOs.Reservas;
using HoteleriaApp.Core.Application.Interfaces;
using HoteleriaApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HoteleriaApp.Core.Application.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepo;
        private readonly IApplicationDbContext _context;

        public ReservaService(IReservaRepository reservaRepo, IApplicationDbContext context)
        {
            _reservaRepo = reservaRepo;
            _context = context;
        }

        public async Task<IReadOnlyList<ReservaDto>> GetAllAsync()
        {
            var reservas = await _reservaRepo.GetAllWithDetailsAsync();
            return reservas.Select(MapToDto).ToList();
        }

        public async Task<ReservaDto?> GetByIdAsync(Guid id)
        {
            var reserva = await _reservaRepo.GetByIdWithDetailsAsync(id);
            return reserva is null ? null : MapToDto(reserva);
        }

        public async Task<IReadOnlyList<HabitacionDisponibleDto>> BuscarDisponibilidadAsync(
            Guid idCategoria, DateOnly fechaEntrada, DateOnly fechaSalida, byte numHuespedes)
        {
            var habitaciones = await _reservaRepo.GetHabitacionesDisponiblesAsync(
                idCategoria, fechaEntrada, fechaSalida, numHuespedes);

            var tarifa = await _context.Tarifas
                .Where(t => t.IdCategoria == idCategoria && t.Activo && t.IdTemporada == null)
                .FirstOrDefaultAsync();

            decimal precioNoche = tarifa?.PrecioNoche ?? 0m;

            return habitaciones.Select(h => new HabitacionDisponibleDto
            {
                Id = h.Id,
                NumeroHabitacion = h.Numero,
                NumeroPiso = h.Piso,
                NombreCategoria = h.TipoHabitacion?.Nombre ?? string.Empty,
                CapacidadMax = h.TipoHabitacion?.CapacidadBase?? 0,
                PrecioNoche = precioNoche
            }).ToList();
        }

        public async Task<(bool Ok, string? Error, ReservaDto? Reserva)> CrearAsync(CrearReservaDto dto, Guid idCliente)
        {
            // 1. Validación de fechas
            if (dto.FechaSalida <= dto.FechaEntrada)
                return (false, "La fecha de salida debe ser posterior a la de entrada.", null);

            // 2. Validar solapamiento
            var solapada = await _reservaRepo.ExisteSolapamientoAsync(
                dto.IdHabitacion, dto.FechaEntrada, dto.FechaSalida);

            if (solapada)
                return (false, "La habitación no está disponible para las fechas seleccionadas.", null);

            // 3. Obtener categoría
            var categoria = await _context.Categories.FindAsync(dto.IdCategoria);
            if (categoria is null)
                return (false, "Categoría no encontrada.", null);

            // 4. Obtener tarifa
            var tarifa = await _context.Tarifas
                .Where(t => t.IdCategoria == dto.IdCategoria && t.Activo && t.IdTemporada == null)
                .FirstOrDefaultAsync();

            // 🔥 5. Precio con fallback (CLAVE)
            decimal precioNoche;

            if (tarifa != null)
            {
                precioNoche = tarifa.PrecioNoche;
            }
            else
            {
                precioNoche = categoria.PricePerNight; // fallback a categoría
            }

            // 🔴 Seguridad extra
            if (precioNoche <= 0)
                return (false, "No se pudo determinar el precio de la habitación.", null);

            // 6. Calcular noches
            int totalNoches = dto.FechaSalida.DayNumber - dto.FechaEntrada.DayNumber;

            if (totalNoches <= 0)
                return (false, "Cantidad de noches inválida.", null);

            // 7. Subtotal habitación
            decimal subtotal = precioNoche * totalNoches;

            // 8. Servicios
            decimal totalServicios = 0m;
            var serviciosAgregar = new List<(Guid IdServicio, decimal Precio)>();

            if (dto.IdsServicios != null && dto.IdsServicios.Any())
            {
                foreach (var idServicio in dto.IdsServicios)
                {
                    var cs = await _context.CategoriasServicio
                        .FirstOrDefaultAsync(x =>
                            x.IdServicio == idServicio &&
                            x.IdCategoria == dto.IdCategoria &&
                            x.Activo);

                    if (cs is null) continue;

                    totalServicios += cs.Precio;
                    serviciosAgregar.Add((idServicio, cs.Precio));
                }
            }

            // 9. Total final
            decimal total = subtotal + totalServicios;

            // 🔴 Seguridad final
            if (total <= 0)
                return (false, "El total de la reserva no puede ser 0.", null);

            // 10. Crear reserva
            var reserva = new Reserva
            {
                NumeroReserva = GenerarNumeroReserva(),
                IdCliente = idCliente,
                IdCategoria = dto.IdCategoria,
                FechaEntrada = dto.FechaEntrada,
                FechaSalida = dto.FechaSalida,
                NumeroHuespedes = dto.NumeroHuespedes,
                Estado = EstadoReserva.Confirmada,

                PrecioBaseNoche = precioNoche,
                TotalNoches = totalNoches,
                SubtotalHabitacion = subtotal,
                TotalServicios = totalServicios,
                Total = total,

                FechaCreacion = DateTime.UtcNow
            };

            // 11. Detalle habitación
            reserva.DetallesReserva.Add(new DetalleReserva
            {
                IdHabitacion = dto.IdHabitacion
            });

            // 12. Servicios
            foreach (var (idServicio, precio) in serviciosAgregar)
            {
                reserva.ReservaServicios.Add(new ReservaServicio
                {
                    IdServicio = idServicio,
                    PrecioAplicado = precio
                });
            }

            // 13. Guardar
            await _reservaRepo.AddAsync(reserva);
            await _reservaRepo.SaveChangesAsync();

            // 14. Obtener con detalles
            var creada = await _reservaRepo.GetByIdWithDetailsAsync(reserva.Id);

            return (true, null, creada is null ? null : MapToDto(creada));
        }

        public async Task<(bool Ok, string? Error)> EditarAsync(Guid id, EditarReservaDto dto)
        {
            var reserva = await _reservaRepo.GetByIdWithDetailsAsync(id);

            if (reserva is null)
                return (false, "Reserva no encontrada.");

            if (reserva.Estado == EstadoReserva.Cancelada || reserva.Estado == EstadoReserva.CheckOut)
                return (false, "No se puede editar una reserva cancelada o con check-out.");

            // 1. Validar fechas
            if (dto.FechaSalida <= dto.FechaEntrada)
                return (false, "La fecha de salida debe ser posterior a la de entrada.");

            // 2. Validar solapamiento
            var solapada = await _reservaRepo.ExisteSolapamientoAsync(
                dto.IdHabitacion, dto.FechaEntrada, dto.FechaSalida, id);

            if (solapada)
                return (false, "La habitación no está disponible para las nuevas fechas.");

            // 3. Obtener categoría
            var categoria = await _context.Categories.FindAsync(dto.IdCategoria);
            if (categoria is null)
                return (false, "Categoría no encontrada.");

            // 4. Obtener tarifa
            var tarifa = await _context.Tarifas
                .Where(t => t.IdCategoria == dto.IdCategoria && t.Activo && t.IdTemporada == null)
                .FirstOrDefaultAsync();

            // 5. Precio con fallback
            decimal precioNoche = tarifa != null ? tarifa.PrecioNoche : categoria.PricePerNight;

            if (precioNoche <= 0)
                return (false, "No se pudo determinar el precio de la habitación.");

            // 6. Calcular noches
            int totalNoches = dto.FechaSalida.DayNumber - dto.FechaEntrada.DayNumber;

            if (totalNoches <= 0)
                return (false, "Cantidad de noches inválida.");

            // 7. Subtotal
            decimal subtotal = precioNoche * totalNoches;

            // 8. Servicios
            decimal totalServicios = 0m;
            var nuevosServicios = new List<ReservaServicio>();

            if (dto.IdsServicios != null && dto.IdsServicios.Any())
            {
                foreach (var idServicio in dto.IdsServicios)
                {
                    var cs = await _context.CategoriasServicio
                        .FirstOrDefaultAsync(x =>
                            x.IdServicio == idServicio &&
                            x.IdCategoria == dto.IdCategoria &&
                            x.Activo);

                    if (cs is null) continue;

                    totalServicios += cs.Precio;

                    nuevosServicios.Add(new ReservaServicio
                    {
                        IdServicio = idServicio,
                        PrecioAplicado = cs.Precio
                    });
                }
            }

            // 9. Total
            decimal total = subtotal + totalServicios;

            if (total <= 0)
                return (false, "El total no puede ser 0.");

            // 🔥 10. ACTUALIZAR RESERVA

            reserva.IdCategoria = dto.IdCategoria;
            reserva.FechaEntrada = dto.FechaEntrada;
            reserva.FechaSalida = dto.FechaSalida;
            reserva.NumeroHuespedes = dto.NumeroHuespedes;

            reserva.PrecioBaseNoche = precioNoche;
            reserva.TotalNoches = totalNoches;
            reserva.SubtotalHabitacion = subtotal;
            reserva.TotalServicios = totalServicios;
            reserva.Total = total;

            // 🔥 VALIDACIÓN CLAVE
            if (dto.IdHabitacion == null)
                return (false, "Debe seleccionar una habitación.");

            // 🔥 11. HABITACIÓN (DETALLE)
            var detalle = reserva.DetallesReserva.FirstOrDefault();

            if (detalle != null)
            {
                detalle.IdHabitacion = dto.IdHabitacion;
            }
            else
            {
                reserva.DetallesReserva.Add(new DetalleReserva
                {
                    IdHabitacion = dto.IdHabitacion
                });
            }

            // 🔥 12. SERVICIOS
            reserva.ReservaServicios.Clear();

            foreach (var servicio in nuevosServicios)
            {
                reserva.ReservaServicios.Add(servicio);
            }

            // 13. Guardar
            _reservaRepo.Update(reserva);
            await _reservaRepo.SaveChangesAsync();

            return (true, null);
        }

        public async Task<EditarReservaDto?> GetEditarByIdAsync(Guid id)
        {
            var reserva = await _reservaRepo.GetByIdWithDetailsAsync(id);
            if (reserva == null) return null;

            // ✅ Seguro contra null
            var detalle = reserva.DetallesReserva.FirstOrDefault();

            return new EditarReservaDto
            {
                IdCategoria = reserva.IdCategoria,
                IdHabitacion = detalle?.IdHabitacion ?? Guid.Empty, // ✅ No explota
                FechaEntrada = reserva.FechaEntrada,
                FechaSalida = reserva.FechaSalida,
                NumeroHuespedes = reserva.NumeroHuespedes
            };
        }


        public async Task<(bool Ok, string? Error)> CancelarAsync(Guid id)
        {
            var reserva = await _reservaRepo.GetByIdAsync(id);
            if (reserva is null)
                return (false, "Reserva no encontrada.");

            if (reserva.Estado == EstadoReserva.Cancelada)
                return (false, "La reserva ya está cancelada.");

            if (reserva.Estado == EstadoReserva.CheckOut)
                return (false, "No se puede cancelar una reserva con check-out completado.");

            reserva.Estado = EstadoReserva.Cancelada;
            reserva.FechaCancelacion = DateTime.UtcNow;

            _reservaRepo.Update(reserva);
            await _reservaRepo.SaveChangesAsync();
            return (true, null);
        }

        private static string GenerarNumeroReserva()
        {
            var now = DateTime.UtcNow;
            return $"RES-{now:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}";
        }

        private static ReservaDto MapToDto(Reserva r)
        {
            var habitacion = r.DetallesReserva.FirstOrDefault()?.Habitacion;
            return new ReservaDto
            {
                Id = r.Id,
                NumeroReserva = r.NumeroReserva,
                NombreCategoria = r.Category?.Name ?? string.Empty,
                NumeroHabitacion = habitacion?.Numero ?? "-",
                FechaEntrada = r.FechaEntrada,
                FechaSalida = r.FechaSalida,
                NumeroHuespedes = r.NumeroHuespedes,
                Estado = r.Estado.ToString(),
                TotalNoches = r.TotalNoches,
                Total = r.Total,
                FechaCreacion = r.FechaCreacion,
                Servicios = r.ReservaServicios
                    .Select(rs => rs.Servicio?.Nombre ?? string.Empty)
                    .Where(n => n.Length > 0)
                    .ToList()
            };
        }


    }
}