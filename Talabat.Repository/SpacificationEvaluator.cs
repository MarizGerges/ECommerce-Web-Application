using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Spacifications;

namespace Talabat.Repository
{
    public static class SpacificationEvaluator<TEntity> where TEntity : BaseEntity
    {
         public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery, ISpacification<TEntity> spec)
        {

            var query = InputQuery;
            if(spec.Criteria is not null) 
                query=query.Where(spec.Criteria);

            if(spec.OrderBy is not null)
                query=query.OrderBy(spec.OrderBy);

            if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if(spec.IsPagenationEnabled)
                query=query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query,(currentQuery , IncludeExpression )=> currentQuery.Include(IncludeExpression));
            return query;

        }
    }
}
