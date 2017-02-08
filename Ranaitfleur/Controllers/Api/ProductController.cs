using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ranaitfleur.Model;
using Ranaitfleur.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Ranaitfleur.Controllers.Api
{
    [Authorize]
    //[RequireHttps]
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IRanaitfleurRepository _repository;

        public ProductController(IRanaitfleurRepository repository, ILogger<ProductController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetAllDresses();

                return Ok(Mapper.Map<IEnumerable<ItemViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all dresses: {ex}");
                return BadRequest("Error occured");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]ItemViewModel item)
        {
            if (ModelState.IsValid)
            {
                var newItem = Mapper.Map<Item>(item);
                _repository.AddDress(newItem);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/products/{item.Name}", Mapper.Map<ItemViewModel>(newItem));
                }
            }

            return BadRequest("Failed to save product");
        }
    }
}
