using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class EspacioService : IEspacioService
    {
        private readonly IGenericRepository<Espacio> _espacioRepository;
        private readonly IMapper _mapper;

        public EspacioService(IGenericRepository<Espacio> espacioRepository, IMapper mapper)
        {
            _espacioRepository = espacioRepository;
            _mapper = mapper;
        }


        public async Task<List<EspacioDTO>> List()
        {
            try
            {
                //revisar este método
                var queryEspacio = await _espacioRepository.Query();

                var espacioList = queryEspacio.ToList();

                return _mapper.Map<List<EspacioDTO>>(espacioList);
            }
            catch
            {
                throw;
            }
        }

        public async Task<EspacioDTO> Create(EspacioDTO model)
        {
            try
            {
                //en el parámetro convierto el DTO a Espacio porque el método trabaja con Model
                var espacioCreated = await _espacioRepository.Create(_mapper.Map<Espacio>(model));

                if (espacioCreated.IdEspacio == 0)
                {
                    throw new TaskCanceledException("No fue posible crear el Espacio");
                }

                return _mapper.Map<EspacioDTO>(espacioCreated);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Edit(EspacioDTO model)
        {
            try
            {
                var espacioModel = _mapper.Map<Espacio>(model);
                var espacioFound = await _espacioRepository.Get(espacio =>
                    espacio.IdEspacio == espacioModel.IdEspacio
                );

                if (espacioFound == null) 
                {
                    throw new TaskCanceledException("El Espacio no existe");
                }

                espacioFound.Nombre = espacioModel.Nombre;
                espacioFound.HorasDisponible = espacioModel.HorasDisponible;
                espacioFound.PrecioPorHora = espacioModel.PrecioPorHora;
                espacioFound.Disponibilidad = espacioModel.Disponibilidad;

                bool response = await _espacioRepository.Edit(espacioFound);

                if (!response)
                {
                    throw new TaskCanceledException("No fue posible editar");
                }

                return response;

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var espacioFound = await _espacioRepository.Get(espacio => espacio.IdEspacio == id);

                if (espacioFound == null)
                {
                    throw new TaskCanceledException("Espacio inexistente");
                }

                bool response = await _espacioRepository.Delete(espacioFound);

                if (!response)
                {
                    throw new TaskCanceledException("No fue posible eliminar el Espacio");
                }

                return response;

            }
            catch
            {
                throw;
            }
        }
    }
}
