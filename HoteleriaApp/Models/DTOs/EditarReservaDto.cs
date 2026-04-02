using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoteleriaApp.Models.DTOs
{
    public class EditarReservaDto
    {
        public int Guid { get; set; } // Mantener Id para identificar la reserva a editar

        [Required(ErrorMessage = "La fecha de entrada es obligatoria")]
        [Display(Name = "Fecha de entrada")]
        public DateOnly FechaEntrada { get; set; }

        [Required(ErrorMessage = "La fecha de salida es obligatoria")]
        [Display(Name = "Fecha de salida")]
        public DateOnly FechaSalida { get; set; }

        [Required(ErrorMessage = "El número de huéspedes es obligatorio")]
        [Range(1, 20, ErrorMessage = "El número de huéspedes debe estar entre 1 y 20")]
        [Display(Name = "Número de huéspedes")]
        public int NumeroHuespedes { get; set; }

        [Display(Name = "Servicios adicionales")]
        public List<int> IdsServicios { get; set; } = new List<int>();
    }
}