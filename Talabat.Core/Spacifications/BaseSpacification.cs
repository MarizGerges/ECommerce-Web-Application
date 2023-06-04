﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Spacifications
{
    public class BaseSpacification<T> : ISpacification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDescending { get; set ; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagenationEnabled { get; set; }

        public BaseSpacification()
        {
        }
        public BaseSpacification( Expression<Func<T,bool>> criteriaExpretion)
        {
            Criteria = criteriaExpretion;
            Includes = new List<Expression<Func<T, object>>>();

        }

        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy= orderByExpression;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderBy = orderByDescExpression;
        }


        public void ApplayPagenation(int skip , int take)
        {
            IsPagenationEnabled = true;
            Skip= skip;
            Take= take;
        }
    }
}
