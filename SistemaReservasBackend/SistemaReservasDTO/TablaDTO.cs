using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasDTO
{
    public class TablaDTO
    {
        public int TotalReservas { get; set; }

        public string? TotalIngresos { get; set; }

        public List<ReservaSemanaDTO> ReservasUltimaSemana { get; set; }
    }
}
