using SistemaReservasDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasBLL.Services.Contract
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> List(int idUsuario);
    }
}
