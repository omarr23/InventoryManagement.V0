using InventoryManagement.DAL.Interfaces;
using InventoryManagement.BLL.manager.services;
using InventoryManagement.DAL.Repository.InventoryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.manager.InventoryService
{
    public class InventoryService : GenericService<Inventory>, IInventoryService
    {
        public InventoryService(IInventoryRepository repository) : base(repository)
        {
        }

        // add Inventory services
    }
}
