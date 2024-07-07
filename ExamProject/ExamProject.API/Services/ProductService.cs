using ExamProject.API.Models.Dtos;
using ExamProject.API.Models.Requests;
using ExamProject.API.Models.Response;
using ExamProject.API.Repositories.Interfaces;
using ExamProject.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

         public async Task<PaginatedProductsResponse<Product>> GetPagedProductsAsync(PaginationProductsRequest<string> paginationRequest)
        {
            return await _productRepository.GetPagedProductsAsync(paginationRequest);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            return await _productRepository.CreateProductAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteProductAsync(id);
        }
    }
}
