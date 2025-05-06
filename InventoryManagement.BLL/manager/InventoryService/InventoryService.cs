using InventoryManagement.DAL.Interfaces;
using InventoryManagement.BLL.manager.services;
using InventoryManagement.DAL.Repository.InventoryRepository;
using InventoryManagement.DAL;

namespace InventoryManagement.BLL.manager.InventoryService
{
    public class InventoryService : GenericService<Inventory>, IInventoryService
    {
        public InventoryService(IInventoryRepository repository, InventoryDbContext context) 
            : base(repository, context)
        {
        }

        // add Inventory services
    }
}
