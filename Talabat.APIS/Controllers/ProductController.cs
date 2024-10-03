using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;

namespace Talabat.APIS.Controllers
{
    public class ProductController : APIBaseController
    {
        private readonly IGenericRepository<Product> _repository;

        public ProductController(IGenericRepository<Product> repository) 
        {
           _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var spec = new ProductSpecifications();
            var product = await _repository.GetAllSpecAsync(spec);
            return Ok(product); // 200
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _repository.GetAsync(id);
            if (product == null)
            {
                return NotFound(new {message = "Not Found", StatusCode = 404});  
            }
            return Ok(product);// 200
        }

    }
}
