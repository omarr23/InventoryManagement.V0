using InventoryManagement.DAL.Models;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.BLL.DTO.ProductDTO;
using InventoryManagement.BLL.Mappers;
using InventoryManagement.DAL.Repository.ProductRepository;
using Microsoft.Extensions.Caching.Memory;
using InventoryManagement.BLL.Helper;
using static InventoryManagement.BLL.DTO.ProductDTO.ProductDTO;

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

        public async Task<Result<IEnumerable<ProductReadDTO>>> GetAllAsync()
        {
            const string cacheKey = "products_all";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<ProductReadDTO>? cachedProducts))
            {
                try
                {
                    var products = await _repository.GetAllAsync();
                    cachedProducts = products.Select(ProductMapper.MapToProductReadDto).ToList();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                    _cache.Set(cacheKey, cachedProducts, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    return Result<IEnumerable<ProductReadDTO>>.Failure($"Error retrieving products: {ex.Message}");
                }
            }

            return Result<IEnumerable<ProductReadDTO>>.Success(cachedProducts!);
        }

        public async Task<Result<ProductReadDTO?>> GetByIdAsync(int id)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id);
                if (product == null)
                    return Result<ProductReadDTO?>.Failure($"Product with ID {id} not found.");

                return Result<ProductReadDTO?>.Success(ProductMapper.MapToProductReadDto(product));
            }
            catch (Exception ex)
            {
                return Result<ProductReadDTO?>.Failure($"Error retrieving product: {ex.Message}");
            }
        }

        public async Task<Result<ProductReadDTO>> AddAsync(ProductCreatDTO dto)
        {
            try
            {
                var product = ProductMapper.MapToProduct(dto);
                await _repository.AddAsync(product);
                await _repository.SaveChangesAsync();

                return Result<ProductReadDTO>.Success(ProductMapper.MapToProductReadDto(product));
            }
            catch (Exception ex)
            {
                return Result<ProductReadDTO>.Failure($"Error adding product: {ex.Message}");
            }
        }

        public async Task<Result<bool>> UpdateAsync(int id, ProductUpdateDTO dto)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id);
                if (product == null)
                    return Result<bool>.Failure($"Product with ID {id} not found.");

                ProductMapper.MapToExistingProduct(dto, product);
                await _repository.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error updating product: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id);
                if (product == null)
                    return Result<bool>.Failure($"Product with ID {id} not found.");

                _repository.Delete(product);
                await _repository.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error deleting product: {ex.Message}");
            }
        }

        public async Task<Result<PaginatedResult<ProductReadDTO>>> GetPaginatedAsync(PaginationParameters parameters)
        {
            try
            {
                var (products, totalCount) = await _repository.GetPaginatedAsync(parameters.PageNumber, parameters.PageSize);
                var totalPages = (int)Math.Ceiling(totalCount / (double)parameters.PageSize);

                return Result<PaginatedResult<ProductReadDTO>>.Success(new PaginatedResult<ProductReadDTO>
                {
                    Items = products.Select(ProductMapper.MapToProductReadDto),
                    TotalCount = totalCount,
                    PageNumber = parameters.PageNumber,
                    PageSize = parameters.PageSize,
                    TotalPages = totalPages
                });
            }
            catch (Exception ex)
            {
                return Result<PaginatedResult<ProductReadDTO>>.Failure($"Error fetching paginated products: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<ProductReadDTO>>> GetSoftDeletedAsync()
        {
            try
            {
                var products = await _repository.GetSoftDeletedAsync();
                return Result<IEnumerable<ProductReadDTO>>.Success(products.Select(ProductMapper.MapToProductReadDto));
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ProductReadDTO>>.Failure($"Error fetching soft-deleted products: {ex.Message}");
            }
        }
    }
}