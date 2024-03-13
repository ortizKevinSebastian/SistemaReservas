using AutoMapper;
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
    public class TablaService : ITablaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IGenericRepository<Espacio> _espacioRepository;
        private readonly IMapper _mapper;

        public TablaService(IReservaRepository reservaRepository, IGenericRepository<Espacio> espacioRepository, IMapper mapper)
        {
            _reservaRepository = reservaRepository;
            _espacioRepository = espacioRepository;
            _mapper = mapper;
        }


        //devuelve un rango de Reservas según el número que le indiquen, lo resta de la última fecha cargada (método base para los siguientes).
        private IQueryable<Reserva> returnRangeReserva(IQueryable<Reserva> tablaReserva, int lessDays)
        {
            DateTime? lastDate = tablaReserva.OrderByDescending(r => r.Fecha).Select(r => r.Fecha).First();
            lastDate = lastDate.Value.AddDays(lessDays);

            return tablaReserva.Where(r => r.Fecha.Value.Date >= lastDate.Value.Date);
        }

        private async Task<int> TotalReservaLastWeek() 
        {
            int total = 0;
            IQueryable<Reserva> _reservaQuery = await _reservaRepository.Query();

            if (_reservaQuery.Count() > 0)
            {
                var tablaReserva = returnRangeReserva(_reservaQuery, -7);
                total = tablaReserva.Count();
            }

            return total;
        }

        private async Task<string> TotalIngresosLastWeek()
        {
            decimal result = 0;
            IQueryable<Reserva> _reservaQuery = await _reservaRepository.Query();

            if (_reservaQuery.Count() > 0)
            {
                var tablaReserva = returnRangeReserva(_reservaQuery, -7);

                result = tablaReserva.Select(r => r.Total).Sum(r => r.Value);
            }

            return Convert.ToString(result, new CultureInfo("es-AR"));
        }

        private async Task<int> TotalEspacio() 
        {
            IQueryable<Espacio> _espacioQuery = await _espacioRepository.Query();
            int total = _espacioQuery.Count();

            return total;
        }

        private async Task<Dictionary<string, int>> ReservasLastWeek() 
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            IQueryable<Reserva> _reservaQuery = await _reservaRepository.Query();

            if (_reservaQuery.Count() > 0)
            {
                var tablaReserva = returnRangeReserva(_reservaQuery, -7);

                result = tablaReserva
                    .GroupBy(r => r.Fecha.Value.Date).OrderBy(g => g.Key)
                    .Select(dr => new { fecha = dr.Key.ToString("dd//MM/yyyy"), total = dr.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
            }

            return result;
        }

        //implementado por la interfaz
        public async Task<TablaDTO> Statistics()
        {
            TablaDTO viewModel = new TablaDTO();

            try
            {
                viewModel.TotalReservas = await TotalReservaLastWeek();
                viewModel.TotalIngresos = await TotalIngresosLastWeek();
                viewModel.TotalEspacios = await TotalEspacio();

                List<ReservaSemanaDTO> listReservaWeek = new List<ReservaSemanaDTO>();

                foreach (KeyValuePair<string, int> item in await ReservasLastWeek())
                {
                    listReservaWeek.Add(new ReservaSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }

                viewModel.ReservasUltimaSemana = listReservaWeek;

            }
            catch
            {
                throw;
            }

            return viewModel;
        }
    }
}
