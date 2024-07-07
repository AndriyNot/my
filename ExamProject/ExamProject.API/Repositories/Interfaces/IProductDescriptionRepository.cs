using ExamProject.API.Models.Dtos;

namespace ExamProject.API.Repositories.Interfaces
{
    public interface IProductDescriptionRepository
    {
        Task<IEnumerable<ProductDescription>> GetProductDescriptionsAsync();
        Task<ProductDescription> GetProductDescriptionByIdAsync(int id);
        Task<ProductDescription> CreateProductDescriptionAsync(ProductDescription productDescription);
        Task UpdateProductDescriptionAsync(ProductDescription productDescription);
        Task DeleteProductDescriptionAsync(int id);
    }
}
