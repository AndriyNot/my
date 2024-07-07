using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string? UserId { get; set; }

        public string? FullName { get; set; }

        public string? City { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
