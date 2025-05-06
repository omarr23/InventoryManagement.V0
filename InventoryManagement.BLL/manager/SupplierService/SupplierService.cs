using InventoryManagement.BLL.manager.services;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.DAL.Repository.SupplierRepository;
using InventoryManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.manager.SupplierService
{
    public class SupplierService : GenericService<Supplier>, ISupplierService
    {
        public SupplierService(ISupplierRepository repository, InventoryDbContext context) 
            : base(repository, context)
        {
        }

        // Implement any additional Supplier-specific business logic here
    }
}
