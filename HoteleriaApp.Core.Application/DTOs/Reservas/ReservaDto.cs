using System;
using System.Collections.Generic;

namespace HoteleriaApp.Core.Application.DTOs.Reservas
{
    public class ReservaDto
    {
        public int Id { get; set; }
        public string NumeroReserva { get; set; } = null!;
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = null!;
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; } = null!;
        public string NumeroHabitacion { get; set; } = null!;
        public DateOnly FechaEntrada { get; set; }
        public DateOnly FechaSalida { get; set; }
        public int NumeroHuespedes { get; set; }
        public string Estado { get; set; } = null!;
        public decimal PrecioBaseNoche { get; set; }
        public int TotalNoches { get; set; }
        public decimal SubtotalHabitacion { get; set; }
        public decimal TotalServicios { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public List<string> Servicios { get; set; } = [];
    }
}