using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecifications
{
    public class ProductSpecifications : BaseSpecification<Product>
    {
        public ProductSpecifications(ProductSpecParams spec) : base(P =>
        (!spec.brandId.HasValue || P.BrandId == spec.brandId.Value) &&
        (!spec.categoryId.HasValue || P.CategoryId == spec.categoryId.Value)
        )
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductCategory);

            if (!string.IsNullOrEmpty(spec.sort))
            {
                switch (spec.sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;

                }

            }
            else
            {
                AddOrderBy(P => P.Name);
            }

        }
        public ProductSpecifications(int id) : base(p => p.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductCategory);
        }
    }
}
