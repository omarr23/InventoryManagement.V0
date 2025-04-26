using InventoryManagement.BLL.manager.services;
using InventoryManagement.DAL.Repository.PaymentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.manager.PaymentService
{
    public class PaymentService : GenericService<Payment>, IPaymentService
    {
        public PaymentService(IPaymentRepository repository) : base(repository)
        {
        }
    }
}
