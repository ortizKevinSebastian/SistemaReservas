using SistemaReservasDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasBLL.Services.Contract
{
    public interface ITablaService
    {
        Task<TablaDTO> Statistics();
    }
}
