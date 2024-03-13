using SistemaReservasDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasBLL.Services.Contract
{
    public interface IEspacioService
    {
        Task<List<EspacioDTO>> List();

        Task<EspacioDTO> Create(EspacioDTO model);

        Task<bool> Edit(EspacioDTO model);

        Task<bool> Delete(int id);
    }
}
