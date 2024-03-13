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
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IGenericRepository<MenuRol> _menuRolRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Usuario> usuarioRepository, 
            IGenericRepository<MenuRol> menuRolRepository, 
            IGenericRepository<Menu> menuRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _menuRolRepository = menuRolRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }


        //obtención de menus según el tipo de usuario.
        public async Task<List<MenuDTO>> List(int idUsuario)
        {
            IQueryable<Usuario> tablaUsuario = await _usuarioRepository.Query(u => u.IdUsuario == idUsuario);
            IQueryable<MenuRol> tablaMenuRol = await _menuRolRepository.Query();
            IQueryable<Menu> tablaMenu = await _menuRepository.Query();

            try 
            {
                IQueryable<Menu> tablaResult = (from u in tablaUsuario
                                                join mr in tablaMenuRol on u.IdRol equals mr.IdRol
                                                join m in tablaMenu on mr.IdMenu equals m.IdMenu
                                                select m).AsQueryable();

                var listMenus = tablaResult.ToList();

                return _mapper.Map<List<MenuDTO>>(listMenus);
            } 
            catch 
            {
                throw;
            }

        }
    }
}
