using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasDTO
{
    public class EspacioDTO
    {
        public int IdEspacio { get; set; }

        public string? Nombre { get; set; }

        public int? HorasDisponible { get; set; }

        public string? PrecioPorHora { get; set; }//se va a ingresar como string pero luego lo transformamos en decimal para la Db

        public int? Disponibilidad { get; set; }//lo cambio a int, en la entidad es bool
    }
}
