using System;
using System.Collections.Generic;

namespace SistemaReservasModel;

public partial class FormaPago
{
    public int IdFormaPago { get; set; }

    public string? Forma { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
