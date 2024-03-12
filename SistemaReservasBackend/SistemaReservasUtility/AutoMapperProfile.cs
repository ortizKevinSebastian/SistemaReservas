using AutoMapper;
using SistemaReservasDTO;
using SistemaReservasModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasUtility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol,RolDTO>().ReverseMap();
            #endregion Rol

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu

            #region Usuario
            //defino en este caso de donde obtengo la información y si necesito convertirla en un valor diferente
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                    destino.RolDescripcion,
                    option => option.MapFrom(origin => origin.IdRolNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.Activo,
                    option => option.MapFrom(origin => origin.Activo == true ? 1 : 0)
                );

            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino =>
                    destino.RolDescripcion,
                    option => option.MapFrom(origin => origin.IdRolNavigation.Nombre)
                );

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                    destino.IdRolNavigation,
                    option => option.Ignore()
                )
                .ForMember(destino =>
                    destino.Activo,
                    option => option.MapFrom(origin => origin.Activo == 1 ? true : false)
                );
            #endregion Usuario

            #region Espacio
            CreateMap<Espacio, EspacioDTO>()
                .ForMember(destino =>
                    destino.PrecioPorHora,
                    option => option.MapFrom(origin => Convert.ToString(origin.PrecioPorHora.Value, new CultureInfo("es-AR")))
                )
                .ForMember(destino =>
                    destino.Disponibilidad,
                    option => option.MapFrom(origin => origin.Disponibilidad == true ? 1 : 0)
                );

            CreateMap<EspacioDTO, Espacio>()
                .ForMember(destino =>
                    destino.PrecioPorHora,
                    option => option.MapFrom(origin => Convert.ToDecimal(origin.PrecioPorHora, new CultureInfo("es-AR")))
                )
                .ForMember(destino =>
                    destino.Disponibilidad,
                    option => option.MapFrom(origin => origin.Disponibilidad == 1 ? true : false)
                );
            #endregion Espacio

            #region Reserva
            CreateMap<Reserva, ReservaDTO>()
                .ForMember(destino =>
                    destino.Total,
                    option => option.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-AR")))
                    )
                .ForMember(destino =>
                    destino.Fecha,
                    option => option.MapFrom(origin => origin.Fecha.Value.ToString("dd/MM/yyyy"))
                    );

            CreateMap<ReservaDTO, Reserva>()
                .ForMember(destino =>
                    destino.Total,
                    option => option.MapFrom(origin => Convert.ToDecimal(origin.Total, new CultureInfo("es-AR")))
                    );
            #endregion Reserva

            #region DetalleReserva
            CreateMap<DetalleReserva, DetalleReservaDTO>()
                .ForMember(destino =>
                    destino.DescripcionEspacio,
                    option => option.MapFrom(origin => origin.IdEspacioNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.PrecioPorHora,
                    option => option.MapFrom(origin => Convert.ToString(origin.PrecioPorHora.Value, new CultureInfo("es-AR")))
                )
                .ForMember(destino =>
                    destino.Total,
                    option => option.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-AR")))
                );

            CreateMap<DetalleReservaDTO, DetalleReserva>()
                .ForMember(destino =>
                    destino.PrecioPorHora,
                    option => option.MapFrom(origin => Convert.ToDecimal(origin.PrecioPorHora, new CultureInfo("es-AR")))
                )
                .ForMember(destino =>
                    destino.Total,
                    option => option.MapFrom(origin => Convert.ToDecimal(origin.PrecioPorHora, new CultureInfo("es-AR")))
                );
            #endregion DetalleReserva

            #region Reporte
            CreateMap<DetalleReserva, ReporteDTO>()
                .ForMember(destino =>
                    destino.FechaRegistro,
                    option => option.MapFrom(origin => origin.IdReservaNavigation.Fecha.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                    destino.Dni,
                    option => option.MapFrom(origin => origin.IdReservaNavigation.Dni)
                )
                .ForMember(destino =>
                    destino.TipoPago,
                    option => option.MapFrom(origin => origin.IdReservaNavigation.IdFormaPagoNavigation.Forma)
                )
                .ForMember(destino =>
                    destino.TotalReserva,
                    option => option.MapFrom(origin => Convert.ToString(origin.IdReservaNavigation.Total.Value, new CultureInfo("es-AR")))
                )
                .ForMember(destino =>
                    destino.Espacio,
                    option => option.MapFrom(origin => origin.IdEspacioNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.Precio,
                    option => option.MapFrom(origin => Convert.ToString(origin.PrecioPorHora.Value, new CultureInfo("es-AR")))
                )
                .ForMember(destino =>
                    destino.Total,
                    option => option.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-AR")))
                );
            #endregion Reporte
        }
    }
}
