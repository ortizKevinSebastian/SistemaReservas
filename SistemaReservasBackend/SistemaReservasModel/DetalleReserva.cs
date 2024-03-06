using System;
using System.Collections.Generic;

namespace SistemaReservasModel;

public partial class DetalleReserva
{
    public int IdDetalleReserva { get; set; }

    public int? IdReserva { get; set; }

    public int? IdEspacio { get; set; }

    public int? CantHoras { get; set; }

    public decimal? PrecioPorHora { get; set; }

    public decimal? Total { get; set; }

    public virtual Espacio? IdEspacioNavigation { get; set; }

    public virtual Reserva? IdReservaNavigation { get; set; }
}
