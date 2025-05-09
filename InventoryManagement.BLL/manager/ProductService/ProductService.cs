using InventoryManagement.DAL.Models;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.BLL.DTO.ProductDTO;
using InventoryManagement.BLL.Mappers;
using InventoryManagement.DAL.Repository.ProductRepository;

namespace InventoryManagement.BLL.manager.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDTO.ProductReadDTO>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(ProductMapper.MapToProductReadDto);
        }

        public async Task<ProductDTO.ProductReadDTO?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product == null ? null : ProductMapper.MapToProductReadDto(product);
        }

        public async Task AddAsync(ProductDTO.ProductCreatDTO dto)
        {
            var product = ProductMapper.MapToProduct(dto);
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
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
