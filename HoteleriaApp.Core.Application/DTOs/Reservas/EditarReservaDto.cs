using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoteleriaApp.Core.Application.DTOs.Reservas
{
    public class EditarReservaDto
    {
        [Required]
        public DateOnly FechaEntrada { get; set; }
        [Required]
        public DateOnly FechaSalida { get; set; }
        [Required, Range(1, 20)]
        public int NumeroHuespedes { get; set; }
        public List<Guid> IdsServicios { get; set; } = [];
    }

}