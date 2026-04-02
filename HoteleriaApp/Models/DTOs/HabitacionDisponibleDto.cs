using System;

namespace HoteleriaApp.Models.DTOs
{
    public class HabitacionDisponibleDto
    {
        public int Id { get; set; }  // útil si luego quieres reservar la habitación

        public string NumeroHabitacion { get; set; } = string.Empty;

        public int NumeroPiso { get; set; }

        public string NombreCategoria { get; set; } = string.Empty;

        public int CapacidadMax { get; set; }

        public decimal PrecioNoche { get; set; }
    }
}