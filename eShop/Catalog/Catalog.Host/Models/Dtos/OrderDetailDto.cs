namespace Catalog.Host.Models.Dtos
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string? UserId { get; set; }
        public int CatalogItemId { get; set; }
        public int Amount { get; set; }
    }
}
