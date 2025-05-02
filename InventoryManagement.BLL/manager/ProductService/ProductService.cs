using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.DAL.Models;
using InventoryManagement.DAL.Interfaces;
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

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _repository.Update(product);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _repository.Delete(product);
            await _repository.SaveChangesAsync();
        }
    }
}
