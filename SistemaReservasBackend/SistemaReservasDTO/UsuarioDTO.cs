using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasDTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string? NombreCompleto { get; set; }

        public string? Correo { get; set; }

        public int? IdRol { get; set; }

        public string? RolDescripcion { get; set; }//agregué este

        public string? Clave { get; set; }

        public int? Activo { get; set; }//cambie bool por int, es mejor trabajar con 0 y 1.
    }
}
