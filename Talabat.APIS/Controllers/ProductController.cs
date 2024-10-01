using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIS.Controllers
{
    public class ProductController : APIBaseController
    {
        private readonly IGenericRepository<Product> _repository;

        public ProductController(IGenericRepository<Product> repository) 
        {
           _repository = repository;
        }



    }
}
