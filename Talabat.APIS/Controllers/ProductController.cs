using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.APIS.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecifications;

namespace Talabat.APIS.Controllers
{
    public class ProductController : APIBaseController
    {
        private readonly GenericRepository<Product> _repository;
        private readonly GenericRepository<ProductBrand> _brandRepo;
        private readonly GenericRepository<ProductCategory> _categoryRepo;
        private readonly IMapper _mapper;

        public ProductController(GenericRepository<Product> ProductRepo, GenericRepository<ProductBrand> BrandRepo, GenericRepository<ProductCategory> CategoryRepo, IMapper mapper)
        {
           _repository = ProductRepo;
           _brandRepo = BrandRepo;
           _categoryRepo = CategoryRepo;
           _mapper = mapper;
        }

        #region GetAllProducts
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAll([FromQuery]ProductSpecParams productSpecParams )
        {
            var spec = new ProductSpecifications(productSpecParams);
            var countSpec = new ProductWithFilterationForCountSpec(productSpecParams);
            var products = await _repository.GetAllSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            var count = await _repository.GetCountAsync(countSpec);
            
            return Ok(new Pagination<ProductDto>(productSpecParams.PageSize , productSpecParams.PageIndex,count , data)); // 200
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
