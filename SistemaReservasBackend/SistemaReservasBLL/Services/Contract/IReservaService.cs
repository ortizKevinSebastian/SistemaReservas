using SistemaReservasDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasBLL.Services.Contract
{
    public interface IReservaService
    {
        Task<ReservaDTO> Register(ReservaDTO model);

        Task<List<ReservaDTO>> History(string searchFor, int dni, string initialDate, string endDate);

        Task<List<ReporteDTO>> Report(string initialDate, string endDate);
    }
}
