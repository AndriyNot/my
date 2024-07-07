using AutoMapper;
using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _catalogTypeRepository;
        private readonly IMapper _mapper;

        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogTypeRepository catalogTypeRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogTypeRepository = catalogTypeRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddCatalogTypeAsync(CatalogTypeDto catalogTypeDto)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var catalogType = _mapper.Map<CatalogType>(catalogTypeDto);
                return await _catalogTypeRepository.AddAsync(catalogType);
            });
        }

        public async Task<bool> DeleteCatalogTypeAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return await _catalogTypeRepository.DeleteAsync(id);
            });
        }

        public async Task<bool> UpdateCatalogTypeAsync(CatalogTypeDto catalogTypeDto)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var catalogType = _mapper.Map<CatalogType>(catalogTypeDto);
                return await _catalogTypeRepository.UpdateAsync(catalogType);
            });
        }
    }
}
