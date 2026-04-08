using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoteleriaApp.Core.Application.DTOs.Reservas
{
    public class EditarReservaDto
    {
        public Guid IdCategoria { get; set; }

        public Guid IdHabitacion { get; set; }

        public DateOnly FechaEntrada { get; set; }

        public DateOnly FechaSalida { get; set; }

        public int NumeroHuespedes { get; set; }

        public List<Guid> IdsServicios { get; set; } = new();
    }

}