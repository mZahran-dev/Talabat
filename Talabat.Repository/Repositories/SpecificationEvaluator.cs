using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Repositories
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> sequence, ISpecification<TEntity> spec)
        {
            var query = sequence; //_dbContext.Set<Product>()
            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);            
            }
            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderBy is not null)
            {
                query = query.OrderByDescending(spec.OrderBy);
            }
            query = spec.Includes.Aggregate(query, (currentQuery, includeExp) => currentQuery.Include(includeExp));
            return query;
        }
    }
}
