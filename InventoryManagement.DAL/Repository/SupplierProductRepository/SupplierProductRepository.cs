using InventoryManagement.DAL.Repositories;
using InventoryManagement.DAL.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DAL.Repository.SupplierProductRepository
{
    public class SupplierProductRepository : GenericRepository<SupplierProduct>, ISupplierProductRepository
    {
        public SupplierProductRepository(InventoryDbContext context) : base(context)
        {
        }
    }
}
