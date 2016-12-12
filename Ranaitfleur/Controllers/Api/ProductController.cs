using Microsoft.AspNetCore.Mvc;
using Ranaitfleur.Model;
using Ranaitfleur.ViewModels;

namespace Ranaitfleur.Controllers.Api
{
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IRanaitfleurRepository _repository;

        public ProductController(IRanaitfleurRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllDresses());
        }

        [HttpPost("")]
        public IActionResult Post([FromBody]ItemViewModel item)
        {
            if (ModelState.IsValid)
            {
                return Created($"api/products/{item.Name}", item);
            }
            return BadRequest(ModelState);
        }
    }
}
