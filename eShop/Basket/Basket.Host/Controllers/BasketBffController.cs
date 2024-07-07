using Basket.Host.Models;
using Basket.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Basket.Host.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class BasketBffController : ControllerBase
{
    private readonly ILogger<BasketBffController> _logger;
    private readonly IBasketService _basketService;

    public BasketBffController(
        ILogger<BasketBffController> logger,
        IBasketService basketService)
    {
        _logger = logger;
        _basketService = basketService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BasketDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBasket([FromBody] BasketRequest request)
    {
        if (string.IsNullOrEmpty(request.BasketId))
        {
            return BadRequest("Basket ID is required.");
        }

        var basket = await _basketService.GetBasket(request.BasketId);
        return Ok(basket);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BasketDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddItemsToBasket([FromBody] AddItemsRequest request)
    {
        if (string.IsNullOrEmpty(request.BasketId))
        {
            return BadRequest("Basket ID is required.");
        }

        var basket = await _basketService.AddItemsToBasket(request.BasketId, request.Items);
        return Ok(basket);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> RemoveItem([FromBody] RemoveItemRequest request)
    {
        var basketId = request.BasketId;
        var catalogItemId = request.CatalogItemId;

        if (string.IsNullOrEmpty(basketId))
        {
            return BadRequest("Basket ID is required.");
        }

        await _basketService.RemoveItem(basketId, catalogItemId);
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> ClearBasket()
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        await _basketService.ClearBasket(basketId!);
        return Ok();
    }
}
