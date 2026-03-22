using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteleriaApp.Core.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal PricePerNight { get; set; }
        public string? Characteristics { get; set; }
        public bool IsActive { get; set; }
        public int Capacidad { get; set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public ICollection<Habitacion> Habitaciones { get; set; } = new List<Habitacion>();
    }
}
