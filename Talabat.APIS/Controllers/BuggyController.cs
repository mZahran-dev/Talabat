using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIS.Controllers
{
    public class BuggyController : APIBaseController
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _dbContext.Products.Find(200);
            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(product);
        }

        [HttpGet("servererror")]
        public ActionResult GetServerErrorRequest()
        {
            var product = _dbContext.Products.Find(100);
            var productDto = product.ToString();
            return Ok(productDto);
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
          return BadRequest(new ApiResponse(400));
        }

        [HttpGet("unauthorized")]
        public ActionResult GetUnAuthorized()
        {
            return Unauthorized(new ApiResponse(401));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }



    }
}
