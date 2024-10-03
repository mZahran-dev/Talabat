using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductSpecifications : BaseSpecification<Product>
    {
        public ProductSpecifications() : base()
        { 
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductCategory);
        }
        public ProductSpecifications(int id) : base(p=>p.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductCategory);
        }
    }
}
