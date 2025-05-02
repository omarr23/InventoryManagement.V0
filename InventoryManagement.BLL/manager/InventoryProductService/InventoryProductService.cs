using InventoryManagement.BLL.manager.services;
using InventoryManagement.DAL.Repository.InventoryProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.DAL.Interfaces;


namespace InventoryManagement.BLL.manager.InventoryProductService
{
    public class InventoryProductService : GenericService<InventoryProduct>, IInventoryProductService
    {
        private readonly IInventoryProductRepository _repository;

        public InventoryProductService(IInventoryProductRepository repository) : base(repository)
        {
            _repository = repository;
        }

        // Get InventoryProduct by ProductId through the repository
        public async Task<InventoryProduct?> GetInventoryProductByProductIdAsync(int productId)
        {
            return await _repository.GetInventoryProductByProductIdAsync(productId);
        }
    }
}
