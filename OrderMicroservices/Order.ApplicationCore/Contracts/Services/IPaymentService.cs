using Order.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.ApplicationCore.Contracts.Services
{
    public interface IPaymentService
    {
        IEnumerable<PaymentMethod> GetPaymentByCustomerId(int customerId);
        PaymentMethod SavePayment(PaymentMethod model);
        bool UpdatePayment(PaymentMethod model);
        bool DeletePayment(int id);
    }
}
