using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasDTO
{
    public class DetalleReservaDTO
    {
        public int? IdEspacio { get; set; }

        public string? DescripcionEspacio { get; set; }//para el front

        public int? CantHoras { get; set; }

        public string? PrecioPorHora { get; set; }//lo voy a convertir luego en decimal

        public string? Total { get; set; }//lo voy a convertir luego en decimal

    }
}
