using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.DAL.Interfaces;

namespace InventoryManagement.DAL.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _context;

        public ProductRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id && !p.IsDeleted);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
// save change get it state -> deleted   , base enetiy 
        public void Delete(Product product)
        {
            product.IsDeleted = true;
            product.DeletedAt = DateTime.UtcNow;
            _context.Products.Update(product);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Products.Where(p => !p.IsDeleted);
            var totalCount = await query.CountAsync();
            
            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task<IEnumerable<Product>> GetSoftDeletedAsync()
        {
            return await _context.Products
                .Where(p => p.IsDeleted)
                .OrderByDescending(p => p.DeletedAt)
                .ToListAsync();
        }
    }
}
