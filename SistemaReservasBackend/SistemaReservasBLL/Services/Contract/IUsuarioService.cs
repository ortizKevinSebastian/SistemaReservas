using SistemaReservasDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasBLL.Services.Contract
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> List();

        Task<SesionDTO> CredentialValidation(string mail, string password);

        Task<UsuarioDTO> Create(UsuarioDTO model);

        Task<bool> Edit(UsuarioDTO model);

        Task<bool> Delete(int id);
    }
}
