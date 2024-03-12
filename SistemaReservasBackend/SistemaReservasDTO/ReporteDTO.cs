using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasDTO
{
    public class ReporteDTO
    {
        public string Dni  { get; set; }

        public string TipoPago { get; set; }

        public string FechaRegistro { get; set; }

        public string TotalReserva { get; set; }

        public string Espacio { get; set; }

        public int HorasDisponible { get; set; }

        public string Precio { get; set; }

        public string Total { get; set; }
    }
}
