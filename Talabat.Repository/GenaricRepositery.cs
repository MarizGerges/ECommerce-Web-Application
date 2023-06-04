using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositries;
using Talabat.Core.Spacifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenaricRepositery<T> : IGenaricRepositery<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenaricRepositery(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T)== typeof(Product))
              return (IReadOnlyList< T >) await _dbContext.products.Include(p=>p.productBrand).Include(p => p.productType).ToListAsync();
            else
                return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
      => await _dbContext.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpacification<T> spec)
        {
           return await ApplaySpacification(spec).ToListAsync();
        }

  
        public async Task<T> GetEntityWithSpecAsync(ISpacification<T> spec)
        {
            return await ApplaySpacification(spec).FirstOrDefaultAsync();

        }

       

        public async Task<int> GetCountWithAsync(ISpacification<T> spec)
        {

            return await ApplaySpacification(spec).CountAsync();
        }

        private IQueryable<T> ApplaySpacification(ISpacification<T> spec)
        {
            return SpacificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
