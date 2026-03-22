using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoteleriaApp.Models.DTOs
{
    public class CrearReservaDto
    {
        [Required]
        public int IdCliente { get; set; }

        [Required]
        public int IdCategoria { get; set; }

        [Required]
        public int IdHabitacion { get; set; }

        [Required]
        public DateOnly FechaEntrada { get; set; }

        [Required]
        public DateOnly FechaSalida { get; set; }

        [Required, Range(1, 20)]
        public int NumeroHuespedes { get; set; }

        public List<int> IdsServicios { get; set; } = new List<int>();
    }
}
