using System;
using System.Collections.Generic;

namespace SistemaReservasModel;

public partial class Espacio
{
    public int IdEspacio { get; set; }

    public string? Nombre { get; set; }

    public int? HorasDisponible { get; set; }

    public decimal? PrecioPorHora { get; set; }

    public bool? Disponibilidad { get; set; }

    public virtual ICollection<DetalleReserva> DetalleReservas { get; set; } = new List<DetalleReserva>();
}
