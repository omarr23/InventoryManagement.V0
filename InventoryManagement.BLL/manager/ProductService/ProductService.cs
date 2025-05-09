using InventoryManagement.DAL.Models;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.BLL.DTO.ProductDTO;
using InventoryManagement.BLL.Mappers;
using InventoryManagement.DAL.Repository.ProductRepository;
using Microsoft.Extensions.Caching.Memory;

namespace InventoryManagement.BLL.manager.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMemoryCache _cache;

        public ProductService(IProductRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

       public async Task<IEnumerable<ProductDTO.ProductReadDTO>> GetAllAsync()
{
    const string cacheKey = "products_all";

    if (!_cache.TryGetValue(cacheKey, out IEnumerable<ProductDTO.ProductReadDTO>? cachedProducts))
    {
        var products = await _repository.GetAllAsync();
        cachedProducts = products.Select(ProductMapper.MapToProductReadDto).ToList();

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

        _cache.Set(cacheKey, cachedProducts, cacheEntryOptions);
    }

    return cachedProducts!;
}

        public async Task<ProductDTO.ProductReadDTO?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product == null ? null : ProductMapper.MapToProductReadDto(product);
        }
        
        public async Task<ProductDTO.ProductReadDTO> AddAsync(ProductDTO.ProductCreatDTO dto)
        {
        var product = ProductMapper.MapToProduct(dto);
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
    
            return ProductMapper.MapToProductReadDto(product);
        }



        public async Task UpdateAsync(int id, ProductDTO.ProductUpdateDTO dto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            ProductMapper.MapToExistingProduct(dto, product);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            _repository.Delete(product);
            await _repository.SaveChangesAsync();
        }

        public async Task<PaginatedResult<ProductDTO.ProductReadDTO>> GetPaginatedAsync(PaginationParameters parameters)
        {
            var (products, totalCount) = await _repository.GetPaginatedAsync(parameters.PageNumber, parameters.PageSize);
            
            var totalPages = (int)Math.Ceiling(totalCount / (double)parameters.PageSize);
            
            return new PaginatedResult<ProductDTO.ProductReadDTO>
            {
                Items = products.Select(ProductMapper.MapToProductReadDto),
                TotalCount = totalCount,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalPages = totalPages
            };
        }

        public async Task<IEnumerable<ProductDTO.ProductReadDTO>> GetSoftDeletedAsync()
        {
            var products = await _repository.GetSoftDeletedAsync();
            return products.Select(ProductMapper.MapToProductReadDto);
        }
    }
}
