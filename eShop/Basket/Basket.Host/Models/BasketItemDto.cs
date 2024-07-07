namespace Basket.Host.Models
{
    public class BasketItemDto
    {
        public int CatalogItemId { get; set; } 

        public string ProductName { get; set; } = null!;  

        public decimal UnitPrice { get; set; }  

        public int Quantity { get; set; }  

        public string PictureUrl { get; set; } = null!;  

        
    }
}
