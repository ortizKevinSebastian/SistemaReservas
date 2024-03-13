using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using SistemaReservasBLL.Services.Contract;
using SistemaReservasDAL.Repositories.Contract;
using SistemaReservasDTO;
using SistemaReservasModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasBLL.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }


        public async Task<List<UsuarioDTO>> List()
        {
            try
            {
                var queryUser = await _usuarioRepository.Query();
                var userList = queryUser.Include(rol => rol.IdRolNavigation).ToList();

                return _mapper.Map<List<UsuarioDTO>>(userList);

            }
            catch 
            {
                throw;
            }
        }

        public async Task<SesionDTO> CredentialValidation(string mail, string password)
        {
            try
            {
                var queryUser = await _usuarioRepository.Query(user =>
                    user.Correo == mail &&
                    user.Clave == password
                );

                if (queryUser.FirstOrDefault() == null)
                {
                    throw new TaskCanceledException("El Usuario no existe");
                }

                Usuario returnUser = queryUser.Include(rol => rol.IdRolNavigation).First();
                
                return _mapper.Map<SesionDTO>(returnUser);
            }
            catch
            {
                throw;
            }
        }

        public async Task<UsuarioDTO> Create(UsuarioDTO model)
        {
            try
            {
                //en el parámetro convierto el DTO a Usuario porque el método trabaja con Model
                var createdUser = await _usuarioRepository.Create(_mapper.Map<Usuario>(model));

                if (createdUser.IdUsuario == 0)
                {
                    throw new TaskCanceledException("El usuario no existe");
                }

                var query = await _usuarioRepository.Query(user => user.IdUsuario == createdUser.IdUsuario);

                createdUser = query.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<UsuarioDTO>(createdUser);

            }
            catch 
            {
                throw;
            }
        }

        public async Task<bool> Edit(UsuarioDTO model)
        {
            try
            {
                //transformo el DTO en Usuario
                var userModel = _mapper.Map<Usuario>(model);

                var userFound = await _usuarioRepository.Get(user => user.IdUsuario == userModel.IdUsuario);

                if (userModel == null)
                {
                    throw new TaskCanceledException("El usuario no existe");                    
                }

                userFound.NombreCompleto = userModel.NombreCompleto;
                userFound.Correo = userModel.Correo;
                userFound.IdRol = userModel.IdRol;
                userFound.Clave = userModel.Clave;
                userFound.Activo = userModel.Activo;

                bool response = await _usuarioRepository.Edit(userFound);

                if (response)
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
                var userFound = await _usuarioRepository.Get(user => user.IdUsuario == id);

                if (userFound == null)
                {
                    throw new TaskCanceledException("El usuario no existe");
                }

                bool response = await _usuarioRepository.Delete(userFound);

                if (!response)
                {
                    throw new TaskCanceledException("No fue posible eliminar");
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
