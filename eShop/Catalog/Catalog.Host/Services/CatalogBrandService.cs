using AutoMapper;
using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
    {
        private readonly ICatalogBrandRepository _catalogBrandRepository;
        private readonly IMapper _mapper;

        public CatalogBrandService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogBrandRepository catalogBrandRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogBrandRepository = catalogBrandRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddCatalogBrandAsync(CatalogBrandDto catalogBrandDto)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var catalogBrand = _mapper.Map<CatalogBrand>(catalogBrandDto);
                return await _catalogBrandRepository.AddAsync(catalogBrand);
            });
        }

        public async Task<bool> DeleteCatalogBrandAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return await _catalogBrandRepository.DeleteAsync(id);
            });
        }

        public async Task<bool> UpdateCatalogBrandAsync(CatalogBrandDto catalogBrandDto)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var catalogBrand = _mapper.Map<CatalogBrand>(catalogBrandDto);
                return await _catalogBrandRepository.UpdateAsync(catalogBrand);
            });
        }
    }
}
