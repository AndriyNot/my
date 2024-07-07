namespace OrderService.Models
{
    public class OrderRequest
    {
        public string? RecipientName { get; set; }
        public string? RecipientEmail { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
    }
}
