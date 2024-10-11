using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;

namespace Talabat.APIS.Controllers
{
    public class ProductController : APIBaseController
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> repository,IMapper mapper) 
        {
           _repository = repository;
           _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var spec = new ProductSpecifications();
            var products = await _repository.GetAllSpecAsync(spec);

            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products)); // 200
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await _repository.GetByIdSpecAsync(spec);


            if (product == null)
            { 
                return NotFound(new ApiResponse(404));  
            }
            return Ok(_mapper.Map<Product,ProductDto>(product));  // 200
        }

    }
}
