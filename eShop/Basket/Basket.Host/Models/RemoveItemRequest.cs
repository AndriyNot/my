namespace Basket.Host.Models
{
    public class RemoveItemRequest
    {
        public string? BasketId { get; set; }
        public int CatalogItemId { get; set; }
    }
}
