﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecifications
{
    public class ProductWithFilterationForCountSpec : BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpec(ProductSpecParams spec) : base(P =>
        (!spec.brandId.HasValue || P.BrandId == spec.brandId.Value) &&
        (!spec.categoryId.HasValue || P.CategoryId == spec.categoryId.Value)
        )
        {

        }
    }
}
