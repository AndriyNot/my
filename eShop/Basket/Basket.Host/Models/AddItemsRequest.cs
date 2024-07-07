namespace Basket.Host.Models
{
    public class AddItemsRequest
    {
        public string BasketId { get; set; } = null!;
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
