using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
          public int? CompanyId { get; set; }
        public virtual Company? Company { get; set; }
    }
}
