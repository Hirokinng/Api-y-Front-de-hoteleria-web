using HoteleriaApp.Core.Domain.Entities;
using HoteleriaApp.Core.Domain.Enums;
using HoteleriaApp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HoteleriaApp.Infrastructure.Seeding
{
    public static class InitialDataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();

            if (!await context.Categories.AnyAsync())
            {
                context.Categories.Add(new Category
                {
                    Name = "Estandar",
                    Description = "Categoria base para habitaciones y reservas.",
                    PricePerNight = 75m,
                    Characteristics = "Confort",
                    IsActive = true,
                    Capacidad = 2
                });

                await context.SaveChangesAsync();
            }

            if (!await context.Clientes.AnyAsync())
            {
                context.Clientes.Add(new Cliente
                {
                    Nombre = "Cliente Demo",
                    Email = "cliente.demo@hotel.local",
                    Telefono = "3000000000",
                    PasswordHash = "demo_hash",
                    FechaRegistro = DateTime.UtcNow,
                    Activo = true
                });

                await context.SaveChangesAsync();
            }

            if (!await context.TiposHabitacion.AnyAsync())
            {
                context.TiposHabitacion.AddRange(
                    new TipoHabitacion
                    {
                        Nombre = "Individual",
                        Descripcion = "Habitacion para 1 persona.",
                        CapacidadBase = 1,
                        PrecioBasePorNoche = 55m
                    },
                    new TipoHabitacion
                    {
                        Nombre = "Doble",
                        Descripcion = "Habitacion para 2 personas.",
                        CapacidadBase = 2,
                        PrecioBasePorNoche = 90m
                    },
                    new TipoHabitacion
                    {
                        Nombre = "Suite",
                        Descripcion = "Habitacion premium para 4 personas.",
                        CapacidadBase = 4,
                        PrecioBasePorNoche = 180m
                    });

                await context.SaveChangesAsync();
            }

            var categoryId = await context.Categories
                .OrderBy(c => c.Id)
                .Select(c => c.Id)
                .FirstAsync();

            if (!await context.Habitaciones.AnyAsync())
            {
                var tipos = await context.TiposHabitacion
                    .OrderBy(t => t.Id)
                    .ToListAsync();

                var primera = tipos[0];
                var segunda = tipos.Count > 1 ? tipos[1] : tipos[0];
                var tercera = tipos.Count > 2 ? tipos[2] : tipos[0];

                context.Habitaciones.AddRange(
                    new Habitacion
                    {
                        Numero = "101",
                        Piso = 1,
                        TipoHabitacionId = primera.Id,
                        Capacidad = primera.CapacidadBase,
                        Estado = HabitacionEstado.Disponible,
                        IdCategoria = categoryId
                    },
                    new Habitacion
                    {
                        Numero = "201",
                        Piso = 2,
                        TipoHabitacionId = segunda.Id,
                        Capacidad = segunda.CapacidadBase,
                        Estado = HabitacionEstado.Disponible,
                        IdCategoria = categoryId
                    },
                    new Habitacion
                    {
                        Numero = "301",
                        Piso = 3,
                        TipoHabitacionId = tercera.Id,
                        Capacidad = tercera.CapacidadBase,
                        Estado = HabitacionEstado.Disponible,
                        IdCategoria = categoryId
                    });

                await context.SaveChangesAsync();
            }

            if (!await context.Reservas.AnyAsync())
            {
                var clienteId = await context.Clientes
                    .OrderBy(c => c.Id)
                    .Select(c => c.Id)
                    .FirstAsync();

                var hoy = DateOnly.FromDateTime(DateTime.Today);
                var totalNoches = 2;
                var precioBase = 90m;
                var subtotal = precioBase * totalNoches;

                context.Reservas.AddRange(
                    new Reserva
                    {
                        NumeroReserva = "RES-1001",
                        IdCliente = clienteId,
                        IdCategoria = categoryId,
                        FechaEntrada = hoy.AddDays(1),
                        FechaSalida = hoy.AddDays(3),
                        NumeroHuespedes = 1,
                        Estado = EstadoReserva.Confirmada,
                        PrecioBaseNoche = precioBase,
                        TotalNoches = totalNoches,
                        SubtotalHabitacion = subtotal,
                        TotalServicios = 0m,
                        Total = subtotal,
                        FechaCreacion = DateTime.UtcNow
                    },
                    new Reserva
                    {
                        NumeroReserva = "RES-1002",
                        IdCliente = clienteId,
                        IdCategoria = categoryId,
                        FechaEntrada = hoy.AddDays(4),
                        FechaSalida = hoy.AddDays(6),
                        NumeroHuespedes = 2,
                        Estado = EstadoReserva.Confirmada,
                        PrecioBaseNoche = precioBase,
                        TotalNoches = totalNoches,
                        SubtotalHabitacion = subtotal,
                        TotalServicios = 0m,
                        Total = subtotal,
                        FechaCreacion = DateTime.UtcNow
                    });

                await context.SaveChangesAsync();
            }
        }
    }
}
