using System;

public class DetalleReserva : BaseEntity
{
    public int IdReserva { get; set; }
    public int IdHabitacion { get; set; }

    public Reserva Reserva { get; set; } = null!;
    public Habitacion Habitacion { get; set; } = null!;
}
