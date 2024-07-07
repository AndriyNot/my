namespace Basket.Host.Models
{
    public class BasketDto
    {
        public string UserId { get; set; } = null!;
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
