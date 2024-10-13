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
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> ProductRepo, IGenericRepository<ProductBrand> BrandRepo, IGenericRepository<ProductCategory> CategoryRepo, IMapper mapper)
        {
           _repository = ProductRepo;
           _brandRepo = BrandRepo;
           _categoryRepo = CategoryRepo;
           _mapper = mapper;
        }

        #region GetAllProducts
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAll(string sort)
        {
            var spec = new ProductSpecifications(sort);
            var products = await _repository.GetAllSpecAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products)); // 200
        } 
        #endregion

        #region GetProductById
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await _repository.GetByIdSpecAsync(spec);


            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Product, ProductDto>(product));  // 200
        }
        #endregion

        #region GetAllBrand
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }
        #endregion

        #region GetAllCategories
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllCategories()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(categories);
        }
        #endregion

    }
}
