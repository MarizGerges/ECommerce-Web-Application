using System.Collections;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositries;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;

        private Hashtable _repositries;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repositries = new Hashtable();




        }

        public IGenaricRepositery<TEntity> Repositery<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if(!_repositries.ContainsKey(type)) 
            {
                var repository = new GenaricRepositery<TEntity>(_dbContext) ;
                _repositries.Add(type, repository);
            }
            return _repositries[type] as IGenaricRepositery<TEntity>;
        }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async void Dispose()
        {
             await _dbContext.DisposeAsync();
        }

        
    }
}
