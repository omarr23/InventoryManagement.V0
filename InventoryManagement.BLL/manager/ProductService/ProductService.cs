using InventoryManagement.BLL.DTO.ProductDTO;
using InventoryManagement.BLL.Helper;
using InventoryManagement.BLL.manager.ProductService;
using InventoryManagement.BLL.Mappers;
using InventoryManagement.DAL.Repository.ProductRepository;
using Microsoft.Extensions.Caching.Memory;
using static InventoryManagement.BLL.DTO.ProductDTO.ProductDTO;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMemoryCache _cache;

    public ProductService(IProductRepository repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<ResultT<IEnumerable<ProductReadDTO>>> GetAllAsync()
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
                return ErrorMassege.Failure("Product.GetAll.Error", $"Error retrieving products: {ex.Message}");
            }
        }

        return ResultT<IEnumerable<ProductReadDTO>>.Success(cachedProducts!);
    }

    public async Task<ResultT<ProductReadDTO?>> GetByIdAsync(int id)
    {
        try
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return ErrorMassege.NotFound("Product.NotFound", $"Product with ID {id} not found.");

            return ResultT<ProductReadDTO?>.Success(ProductMapper.MapToProductReadDto(product));
        }
        catch (Exception ex)
        {
            return ErrorMassege.Failure("Product.GetById.Error", $"Error retrieving product: {ex.Message}");
        }
    }

    public async Task<ResultT<ProductReadDTO>> AddAsync(ProductCreatDTO dto)
    {
        try
        {
            var product = ProductMapper.MapToProduct(dto);
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

            return ResultT<ProductReadDTO>.Success(ProductMapper.MapToProductReadDto(product));
        }
        catch (Exception ex)
        {
            return ErrorMassege.Failure("Product.Add.Error", $"Error adding product: {ex.Message}");
        }
    }

    public async Task<ResultT<bool>> UpdateAsync(int id, ProductUpdateDTO dto)
    {
        try
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return ErrorMassege.NotFound("Product.Update.NotFound", $"Product with ID {id} not found.");

            ProductMapper.MapToExistingProduct(dto, product);
            await _repository.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            return ErrorMassege.Failure("Product.Update.Error", $"Error updating product: {ex.Message}");
        }
    }

    public async Task<ResultT<bool>> DeleteAsync(int id)
    {
        try
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return ErrorMassege.NotFound("Product.Delete.NotFound", $"Product with ID {id} not found.");

            _repository.Delete(product);
            await _repository.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return ErrorMassege.Failure("Product.Delete.Error", $"Error deleting product: {ex.Message}");
        }
    }

    public async Task<ResultT<PaginatedResult<ProductReadDTO>>> GetPaginatedAsync(PaginationParameters parameters)
    {
        try
        {
            var (products, totalCount) = await _repository.GetPaginatedAsync(parameters.PageNumber, parameters.PageSize);
            var totalPages = (int)Math.Ceiling(totalCount / (double)parameters.PageSize);

            return new PaginatedResult<ProductReadDTO>
            {
                Items = products.Select(ProductMapper.MapToProductReadDto),
                TotalCount = totalCount,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalPages = totalPages
            };
        }
        catch (Exception ex)
        {
            return ErrorMassege.Failure("Product.Pagination.Error", $"Error fetching paginated products: {ex.Message}");
        }
    }

    public async Task<ResultT<IEnumerable<ProductReadDTO>>> GetSoftDeletedAsync()
    {
        try
        {
            var products = await _repository.GetSoftDeletedAsync();
            return products.Select(ProductMapper.MapToProductReadDto).ToList();
        }
        catch (Exception ex)
        {
            return ErrorMassege.Failure("Product.GetSoftDeleted.Error", $"Error fetching soft-deleted products: {ex.Message}");
        }
    }
}
