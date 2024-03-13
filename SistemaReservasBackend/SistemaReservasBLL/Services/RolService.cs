using AutoMapper;
using SistemaReservasBLL.Services.Contract;
using SistemaReservasDAL.Repositories.Contract;
using SistemaReservasDTO;
using SistemaReservasModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasBLL.Services
{
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Rol> _rolRepository;
        private readonly IMapper _mapper;

        public RolService(IGenericRepository<Rol> rolRepository, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
        }

        //Rol a RolDTO en forma de lista.
        public async Task<List<RolDTO>> List()
        {
            try
            {
                var listRoles = await _rolRepository.Query();
                return _mapper.Map<List<RolDTO>>(listRoles);
            }
            catch 
            {
                throw;
            }
        }
    }
}
