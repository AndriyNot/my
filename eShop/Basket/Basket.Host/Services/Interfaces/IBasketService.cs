using Basket.Host.Models;

namespace Basket.Host.Services.Interfaces;

public interface IBasketService
{
    Task<BasketDto> GetBasket(string basketId);
    Task<BasketDto> AddItemsToBasket(string basketId, List<BasketItemDto> items);
    Task RemoveItem(string userId, int catalogItemId);
    Task ClearBasket(string userId);
}