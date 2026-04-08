using System;
using System.Collections.Generic;

namespace HoteleriaApp.Core.Application.DTOs.Reservas
{
    public class ReservaDto
    {
        public Guid Id { get; set; }
        public string NumeroReserva { get; set; } = null!;
        public string NombreCategoria { get; set; } = null!;
        public string NumeroHabitacion { get; set; } = null!;
        public DateOnly FechaEntrada { get; set; }
        public DateOnly FechaSalida { get; set; }
        public int NumeroHuespedes { get; set; }
        public string Estado { get; set; } = null!;
        public int TotalNoches { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<string> Servicios { get; set; } = [];
        public Guid IdCategoria { get; set; }
        public Guid IdHabitacion { get; set; }
    }
}