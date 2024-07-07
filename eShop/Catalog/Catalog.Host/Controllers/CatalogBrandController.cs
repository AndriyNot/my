using Catalog.Host.Models.Dtos;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ILogger<CatalogBrandController> _logger;
    private readonly ICatalogBrandService _catalogBrandService;

    public CatalogBrandController(
        ILogger<CatalogBrandController> logger,
        ICatalogBrandService catalogBrandService)
    {
        _logger = logger;
        _catalogBrandService = catalogBrandService;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddCatalogBrand([FromBody] CatalogBrandDto catalogBrandDto)
    {
        var result = await _catalogBrandService.AddCatalogBrandAsync(catalogBrandDto);
        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteCatalogBrand(int id)
    {
        var result = await _catalogBrandService.DeleteCatalogBrandAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateCatalogBrand([FromBody] CatalogBrandDto catalogBrandDto)
    {
        var result = await _catalogBrandService.UpdateCatalogBrandAsync(catalogBrandDto);
        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }
}