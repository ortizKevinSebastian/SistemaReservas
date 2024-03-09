using Microsoft.EntityFrameworkCore;
using SistemaReservasDAL.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasDAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly DbReservaContext _dbContext;

        public GenericRepository(DbReservaContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<TModel> Get(Expression<Func<TModel, bool>> filter)
        {
            try
            {
                TModel model = await _dbContext.Set<TModel>().FirstOrDefaultAsync(filter); //el modelo se especifica en otra capa
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TModel> Create(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Edit(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Remove(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<TModel>> Query(Expression<Func<TModel, bool>> filter = null)//deveulve la consulta para que quien lo llame sea el que lo ejecute, a diferencia de los anteriores métodos que son ejecuciones
        {
            try
            {
                IQueryable<TModel> queryModel = filter == null? //no hay ningun filtro
                _dbContext.Set<TModel>() : _dbContext.Set<TModel>().Where(filter); //devuelve el model
                return queryModel;
            }
            catch
            {
                throw;
            }
        }
    }
}
