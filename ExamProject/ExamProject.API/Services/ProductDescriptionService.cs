using ExamProject.API.Models.Dtos;
using ExamProject.API.Repositories.Interfaces;
using ExamProject.API.Services.Interfaces;

namespace ExamProject.API.Services
{
    public class ProductDescriptionService : IProductDescriptionService
    {
        private readonly IProductDescriptionRepository _productDescriptionRepository;

        public ProductDescriptionService(IProductDescriptionRepository productDescriptionRepository)
        {
            _productDescriptionRepository = productDescriptionRepository;
        }

        public async Task<IEnumerable<ProductDescription>> GetProductDescriptionsAsync()
        {
            return await _productDescriptionRepository.GetProductDescriptionsAsync();
        }

        public async Task<ProductDescription> GetProductDescriptionByIdAsync(int id)
        {
            return await _productDescriptionRepository.GetProductDescriptionByIdAsync(id);
        }

        public async Task<ProductDescription> CreateProductDescriptionAsync(ProductDescription productDescription)
        {
            return await _productDescriptionRepository.CreateProductDescriptionAsync(productDescription);
        }

        public async Task UpdateProductDescriptionAsync(ProductDescription productDescription)
        {
            await _productDescriptionRepository.UpdateProductDescriptionAsync(productDescription);
        }

        public async Task DeleteProductDescriptionAsync(int id)
        {
            await _productDescriptionRepository.DeleteProductDescriptionAsync(id);
        }
    }
}
