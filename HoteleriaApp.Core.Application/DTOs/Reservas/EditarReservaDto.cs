using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoteleriaApp.Core.Application.DTOs.Reservas
{
    public class EditarReservaDto
    {
        public int Id { get; set; }

        [Required]
        public DateOnly FechaEntrada { get; set; }

        [Required]
        public DateOnly FechaSalida { get; set; }

        [Required, Range(1, 20)]
        public int NumeroHuespedes { get; set; }

        public List<int> IdsServicios { get; set; } = [];
    }

}