﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;

        public GenericRepository(StoreContext Dbcontext) 
        {
            _dbcontext = Dbcontext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // to solve this issue of condition => implement Specification design pattern
            if(typeof(T) == typeof(Product))
            {
                return  (IEnumerable<T>) await _dbcontext.Set<Product>().Include(p => p.ProductBrand).Include(p => p.ProductCategory).ToListAsync();
            }
            return await _dbcontext.Set<T>().ToListAsync();
        }

 

        public async Task<T> GetAsync(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                return await _dbcontext.Set<Product>().Where(p => p.Id == id).Include(p => p.ProductBrand).Include(p => p.ProductCategory).FirstOrDefaultAsync() as T;
            }
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        //Get With specification
        public async Task<IEnumerable<T>> GetAllSpecAsync(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbcontext.Set<T>(), spec).ToListAsync();
        }
        async Task<IEnumerable<T>> IGenericRepository<T>.GetByIdSpecAsync(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbcontext.Set<T>(), spec).ToListAsync();
        }
    }
}
