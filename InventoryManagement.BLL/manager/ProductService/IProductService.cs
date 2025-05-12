using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.BLL.DTO.ProductDTO;
using InventoryManagement.DAL.Models;
using InventoryManagement.BLL.Helper;


namespace InventoryManagement.BLL.manager.ProductService
{
    public interface IProductService
    {
        Task<Result<IEnumerable<ProductDTO.ProductReadDTO>>> GetAllAsync();
        Task<Result<ProductDTO.ProductReadDTO?>> GetByIdAsync(int id);
        Task<Result<ProductDTO.ProductReadDTO>> AddAsync(ProductDTO.ProductCreatDTO dto);

        Task<Result<bool>> UpdateAsync(int id, ProductDTO.ProductUpdateDTO dto);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<PaginatedResult<ProductDTO.ProductReadDTO>>> GetPaginatedAsync(PaginationParameters parameters);
        Task<Result<IEnumerable<ProductDTO.ProductReadDTO>>> GetSoftDeletedAsync();
    }
}

