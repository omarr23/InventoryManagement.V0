using InventoryManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DAL.Repository.SupplierRepository
{

    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        // Add here any Supplier-specific methods if needed
    }
}
