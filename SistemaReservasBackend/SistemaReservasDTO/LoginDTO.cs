using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasDTO
{
    public class LoginDTO//recibe las credenciales
    {
        public string? Correo { get; set; }

        public string? Clave { get; set; }
    }
}
