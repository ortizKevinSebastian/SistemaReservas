using SistemaReservasModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasDAL.Repositories.Contract
{
    public interface IReservaRepository : IGenericRepository<Reserva>
    {
        Task<Reserva> Register(Reserva reserva);
    }
}
