using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasDTO
{
    public class ReservaDTO
    {
        public int IdReserva { get; set; }

        public int? Dni { get; set; }

        public int? Tel { get; set; }

        public int? IdFormaPago { get; set; }//REVISAR ESTO

        public string? Total { get; set; }//luego a decimal

        public string? Fecha { get; set; }//lo cambio a string

        public virtual ICollection<DetalleReservaDTO> DetalleReservas { get; set; } //llama al DTO

    }

}
