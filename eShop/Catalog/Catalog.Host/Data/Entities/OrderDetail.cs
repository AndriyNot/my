using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Data.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string? UserId { get; set; }

        public int CatalogItemId { get; set; }

        public CatalogItem CatalogItem { get; set; } = null!;

        public int Amount { get; set; }
    }
}
