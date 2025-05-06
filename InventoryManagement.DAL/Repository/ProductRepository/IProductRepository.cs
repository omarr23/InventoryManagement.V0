using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.DAL.Models;

namespace InventoryManagement.DAL.Repository.ProductRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        void Update(Product product);
        void Delete(Product product);
        Task SaveChangesAsync();
        Task<(IEnumerable<Product> Products, int TotalCount)> GetPaginatedAsync(int pageNumber, int pageSize);
    }
}
