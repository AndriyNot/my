using ExamProject.API.Data;
using ExamProject.API.Models.Dtos;
using ExamProject.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                // Логика обработки отсутствия категории, например, выброс исключения или возврат другого значения по умолчанию
                throw new InvalidOperationException($"Category with id {id} not found.");
            }
            return category;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
