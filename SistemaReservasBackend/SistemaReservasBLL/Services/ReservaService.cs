using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaReservasBLL.Services.Contract;
using SistemaReservasDAL.Repositories.Contract;
using SistemaReservasDTO;
using SistemaReservasModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasBLL.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IGenericRepository<DetalleReserva> _detatalleReservaRepository;
        private readonly IMapper _mapper;

        public ReservaService(IReservaRepository reservaRepository, IGenericRepository<DetalleReserva> detatalleReservaRepository, IMapper mapper)
        {
            _reservaRepository = reservaRepository;
            _detatalleReservaRepository = detatalleReservaRepository;
            _mapper = mapper;
        }

        public async Task<ReservaDTO> Register(ReservaDTO model)
        {
            try
            {
                var reserva = await _reservaRepository.Register(_mapper.Map<Reserva>(model));

                if (reserva.IdReserva == 0)
                {
                    throw new TaskCanceledException("No fue posible realizar la reserva");
                }

                return _mapper.Map<ReservaDTO>(reserva);
            }
            catch
            {
                throw;
            }
        }

        //posiblemente no lo use.
        public async Task<List<ReservaDTO>> History(string searchFor, int dni, string initial, string final)
        {
            IQueryable<Reserva> query = await _reservaRepository.Query();
            var listReserva = new List<Reserva>();

            try
            {
                if (searchFor == "fecha")
                {
                    DateTime initialDate = DateTime.ParseExact(initial, "dd/MM/yyyy", new CultureInfo("es-AR"));
                    DateTime finalDate = DateTime.ParseExact(final, "dd/MM/yyyy", new CultureInfo("es-AR"));

                    //búsqueda por fecha
                    listReserva = await query.Where(x =>
                        x.Fecha.Value.Date >= initialDate.Date &&
                        x.Fecha.Value.Date <= finalDate.Date
                    ).Include(dr => dr.DetalleReservas)
                    .ThenInclude(p => p.IdEspacioNavigation)
                    .ToListAsync();
                }
                else
                {
                    //busqueda por dni
                    listReserva = await query.Where(x =>
                        x.Dni == dni 
                        ).Include(d => d.DetalleReservas)
                    .ThenInclude(p => p.IdEspacioNavigation)
                    .ToListAsync();
                }
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<ReservaDTO>>(listReserva);
        }

        public async Task<List<ReporteDTO>> Report(string initial, string final)
        {
            IQueryable<DetalleReserva> query = await _detatalleReservaRepository.Query();
            var listReserva = new List<DetalleReserva>();

            try
            {
                DateTime initialDate = DateTime.ParseExact(initial, "dd/MM/yyyy", new CultureInfo("es-AR"));
                DateTime finalDate = DateTime.ParseExact(final, "dd/MM/yyyy", new CultureInfo("es-AR"));

                listReserva = await query
                    .Include(e => e.IdEspacioNavigation)
                    .Include(r => r.IdReservaNavigation)
                    .Where(dr =>
                        dr.IdReservaNavigation.Fecha.Value.Date >= initialDate.Date &&
                        dr.IdReservaNavigation.Fecha.Value.Date <= finalDate.Date
                    ).ToListAsync();
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<ReporteDTO>>(listReserva); 
        }
    }
}
