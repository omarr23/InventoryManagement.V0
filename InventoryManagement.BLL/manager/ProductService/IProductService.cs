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
        Task<ResultT<IEnumerable<ProductDTO.ProductReadDTO>>> GetAllAsync();
        Task<ResultT<ProductDTO.ProductReadDTO?>> GetByIdAsync(int id);
        Task<ResultT<ProductDTO.ProductReadDTO>> AddAsync(ProductDTO.ProductCreatDTO dto);

        Task<ResultT<bool>> UpdateAsync(int id, ProductDTO.ProductUpdateDTO dto);
        Task<ResultT<bool>> DeleteAsync(int id);
        Task<ResultT<PaginatedResult<ProductDTO.ProductReadDTO>>> GetPaginatedAsync(PaginationParameters parameters);
        Task<ResultT<IEnumerable<ProductDTO.ProductReadDTO>>> GetSoftDeletedAsync();
    }
}

