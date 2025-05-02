using InventoryManagement.DAL.Interfaces;
using InventoryManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DAL.Repository.SupplierRepository
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(InventoryDbContext context) : base(context)
        {
        }

        // Implement Supplier-specific methods here if needed
    }
}
