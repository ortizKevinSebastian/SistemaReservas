using System;
using System.Collections.Generic;

namespace SistemaReservasModel;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int? Dni { get; set; }

    public int? Tel { get; set; }

    public int? IdFormaPago { get; set; }

    public decimal? Total { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual ICollection<DetalleReserva> DetalleReservas { get; set; } = new List<DetalleReserva>();

    public virtual FormaPago? IdFormaPagoNavigation { get; set; }
}
