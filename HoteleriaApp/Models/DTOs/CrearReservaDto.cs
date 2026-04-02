using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoteleriaApp.Models.DTOs
{
    public class CrearReservaDto
    {
        [Required]
        public Guid IdCliente { get; set; }

        [Required]
        public Guid IdCategoria { get; set; }

        [Required]
        public Guid IdHabitacion { get; set; }

        [Required]
        public DateOnly FechaEntrada { get; set; }

        [Required]
        public DateOnly FechaSalida { get; set; }

        [Required, Range(1, 20)]
        public int NumeroHuespedes { get; set; }

        public List<int> IdsServicios { get; set; } = new List<int>();
    }
}
