using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Spacifications;

namespace Talabat.Core.Repositries
{
    public interface IGenaricRepositery <T> where T: BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpacification<T> spec);
        Task<T> GetEntityWithSpecAsync(ISpacification<T> spec);

        Task<int> GetCountWithAsync(ISpacification<T> spec);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
