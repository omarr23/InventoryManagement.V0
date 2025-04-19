using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InventoryManagement.DAL
{
    public class InventoryDbContextFactory : IDesignTimeDbContextFactory<InventoryDbContext>
    {
        public InventoryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();

            // connection
            optionsBuilder.UseSqlServer("Server=DESKTOP-NASUEKH\\SQLEXPRESS;Database=INV0;Trusted_Connection=True;TrustServerCertificate=True;");

            return new InventoryDbContext(optionsBuilder.Options);
        }
    }
}


