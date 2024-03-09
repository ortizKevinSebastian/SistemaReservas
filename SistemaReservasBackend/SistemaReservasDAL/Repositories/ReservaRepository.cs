using SistemaReservasDAL.Repositories.Contract;
using SistemaReservasModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasDAL.Repositories
{
    public class ReservaRepository : GenericRepository<Reserva>, IReservaRepository
    {
        private readonly DbReservaContext _dbContext;

        public ReservaRepository(DbReservaContext dbContext) : base(dbContext) //le paso el contexto porque lo demanda tambien el padre (base)
        {
            _dbContext = dbContext;
        }

        public async Task<Reserva> Register(Reserva reserva)
        {
            Reserva reservaOk = new Reserva();
            //esté método afecta a la clase de Espacios, a las Reservas por lo tanto son varios registros para que todo salga bien hago lo siguiente. Uso Transaction
            //si dentro de la lógica hay un error, restablece todo como está al principio.
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    reservaOk = reserva;

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                return reservaOk;
            }          

        }
    }
}
