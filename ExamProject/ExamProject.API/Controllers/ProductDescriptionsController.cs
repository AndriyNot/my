using ExamProject.API.Models.Dtos;
using ExamProject.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDescriptionsController : ControllerBase
    {
        private readonly IProductDescriptionService _productDescriptionService;

        public ProductDescriptionsController(IProductDescriptionService productDescriptionService)
        {
            _productDescriptionService = productDescriptionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDescription>>> GetProductDescriptions()
        {
            var productDescriptions = await _productDescriptionService.GetProductDescriptionsAsync();
            return Ok(productDescriptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDescription>> GetProductDescription(int id)
        {
            var productDescription = await _productDescriptionService.GetProductDescriptionByIdAsync(id);
            if (productDescription == null)
            {
                return NotFound();
            }
            return Ok(productDescription);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDescription>> CreateProductDescription(ProductDescription productDescription)
        {
            var createdProductDescription = await _productDescriptionService.CreateProductDescriptionAsync(productDescription);
            return CreatedAtAction(nameof(GetProductDescription), new { id = createdProductDescription.Id }, createdProductDescription);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductDescription(int id, ProductDescription productDescription)
        {
            if (id != productDescription.Id)
            {
                return BadRequest();
            }

            await _productDescriptionService.UpdateProductDescriptionAsync(productDescription);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDescription(int id)
        {
            var productDescription = await _productDescriptionService.GetProductDescriptionByIdAsync(id);
            if (productDescription == null)
            {
                return NotFound();
            }

            await _productDescriptionService.DeleteProductDescriptionAsync(id);

            return NoContent();
        }
    }
}
