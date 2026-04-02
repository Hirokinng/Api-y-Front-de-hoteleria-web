namespace HoteleriaApp.Core.Application.DTOs.Reservas
{

    public class HabitacionDisponibleDto
    {
        public Guid Id { get; set; }
        public string NumeroHabitacion { get; set; } = null!;
        public int NumeroPiso { get; set; }
        public string NombreCategoria { get; set; } = null!;
        public int CapacidadMax { get; set; }
        public decimal PrecioNoche { get; set; }
    }

}
