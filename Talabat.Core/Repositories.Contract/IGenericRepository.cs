﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        //Task<T> GetAsync(int id);
        //Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetAllSpecAsync(ISpecification<T> spec);
        public Task<T> GetByIdSpecAsync(ISpecification<T> spec);
    }
}
