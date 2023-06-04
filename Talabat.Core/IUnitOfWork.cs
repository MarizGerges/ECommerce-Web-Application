using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositries;

namespace Talabat.Core
{
    public interface IUnitOfWork: IDisposable 
    {
        IGenaricRepositery<TEntity> Repositery<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();



    }
}
