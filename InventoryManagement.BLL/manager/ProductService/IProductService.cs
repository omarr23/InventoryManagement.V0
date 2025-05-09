using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.BLL.DTO.ProductDTO;
using InventoryManagement.DAL.Models;


namespace InventoryManagement.BLL.manager.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO.ProductReadDTO>> GetAllAsync();
        Task<ProductDTO.ProductReadDTO?> GetByIdAsync(int id);
        Task AddAsync(ProductDTO.ProductCreatDTO dto);
        Task UpdateAsync(int id, ProductDTO.ProductUpdateDTO dto);
        Task DeleteAsync(int id);
        Task<PaginatedResult<ProductDTO.ProductReadDTO>> GetPaginatedAsync(PaginationParameters parameters);
        Task<IEnumerable<ProductDTO.ProductReadDTO>> GetSoftDeletedAsync();
    }
}

