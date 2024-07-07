using Basket.Host.Models;
using Basket.Host.Services.Interfaces;
using System.Collections.Concurrent;

namespace Basket.Host.Services;

public class BasketService : IBasketService
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<BasketService> _logger;

    public BasketService(ICacheService cacheService, ILogger<BasketService> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<BasketDto> GetBasket(string basketId)
    {
        _logger.LogInformation($"Attempting to get basket for ID: {basketId}");
        var basket = await _cacheService.GetAsync<BasketDto>(basketId);
        if (basket == null)
        {
            _logger.LogInformation($"Basket not found for ID: {basketId}. Creating new basket.");
            basket = new BasketDto
            {
                UserId = basketId,
                Items = new List<BasketItemDto>()
            };
            await _cacheService.AddOrUpdateAsync(basketId, basket);
        }
        else
        {
            _logger.LogInformation($"Basket found for ID: {basketId}");
        }

        return basket;
    }

    public async Task<BasketDto> AddItemsToBasket(string basketId, List<BasketItemDto> items)
    {
        _logger.LogInformation($"Attempting to add items to basket for ID: {basketId}");
        var basket = await GetBasket(basketId);

        foreach (var item in items)
        {
            var existingItem = basket.Items.FirstOrDefault(i => i.CatalogItemId == item.CatalogItemId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                basket.Items.Add(item);
            }
        }

        await _cacheService.AddOrUpdateAsync(basketId, basket);
        _logger.LogInformation($"Items added to basket for ID: {basketId}");
        return basket;
    }

    public async Task RemoveItem(string userId, int catalogItemId)
    {
        _logger.LogInformation($"Attempting to remove item from basket for ID: {userId}");
        var basket = await GetBasket(userId);
        if (basket != null)
        {
            basket.Items.RemoveAll(item => item.CatalogItemId == catalogItemId);
            await _cacheService.AddOrUpdateAsync(userId, basket);
            _logger.LogInformation($"Item removed from basket for ID: {userId}");
        }
    }

    public async Task ClearBasket(string userId)
    {
        _logger.LogInformation($"Attempting to clear basket for ID: {userId}");
        await _cacheService.AddOrUpdateAsync(userId, new BasketDto { UserId = userId, Items = new List<BasketItemDto>() });
        _logger.LogInformation($"Basket cleared for ID: {userId}");
    }
}