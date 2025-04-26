using InventoryManagement.BLL.manager.services;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.manager.SupplierProductService
{
    public class SupplierProductService : GenericService<SupplierProduct>, ISupplierProductService
    {
        public SupplierProductService(IGenericRepository<SupplierProduct> repository) : base(repository)
        {
        }
    }
}
