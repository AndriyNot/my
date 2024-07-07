using Catalog.Host.Models.Dtos;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogTypeController(
        ILogger<CatalogTypeController> logger,
        ICatalogTypeService catalogTypeService)
    {
        _logger = logger;
        _catalogTypeService = catalogTypeService;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddCatalogType([FromBody] CatalogTypeDto catalogTypeDto)
    {
        var result = await _catalogTypeService.AddCatalogTypeAsync(catalogTypeDto);
        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteCatalogType(int id)
    {
        var result = await _catalogTypeService.DeleteCatalogTypeAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateCatalogType([FromBody] CatalogTypeDto catalogTypeDto)
    {
        var result = await _catalogTypeService.UpdateCatalogTypeAsync(catalogTypeDto);
        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }
}