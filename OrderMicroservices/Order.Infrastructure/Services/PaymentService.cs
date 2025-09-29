using Order.ApplicationCore.Contracts.Repositories;
using Order.ApplicationCore.Contracts.Services;
using Order.ApplicationCore.Entities;
using Order.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _payments;
        public PaymentService(IPaymentRepository payments)
        {
            _payments = payments;
        }
        public bool DeletePayment(int id)
        {
            var existing = _payments.GetById(id);
            if (existing == null) return false;
            _payments.DeleteById(id);
            return true;
        }

        public IEnumerable<PaymentMethod> GetPaymentByCustomerId(int customerId)
        {
            return _payments.GetByCustomerId(customerId);
        }

        public PaymentMethod SavePayment(PaymentMethod model)
        {
            if (model.IsDefault)
            {
                var existingForCustomer = _payments
                    .GetByCustomerId(model.CustomerId)
                    .Where(p => p.IsDefault);

                foreach (var pm in existingForCustomer)
                {
                    pm.IsDefault = false;
                    _payments.Update(pm);
                }
            }
            var created = _payments.Insert(model);
            return created;
        }

        public bool UpdatePayment(PaymentMethod model)
        {
            var existing = _payments.GetById(model.Id);
            if (existing == null) return false;

            if (model.IsDefault)
            {
                var others = _payments
                    .GetByCustomerId(model.CustomerId)
                    .Where(p => p.IsDefault && p.Id != model.Id);

                foreach (var pm in others)
                {
                    pm.IsDefault = false;
                    _payments.Update(pm);
                }
            }

            existing.PaymentTypeId = model.PaymentTypeId;
            existing.Provider = model.Provider;
            existing.AccountNumber = model.AccountNumber;
            existing.Expiry = model.Expiry;
            existing.IsDefault = model.IsDefault;
            existing.CustomerId = model.CustomerId;

            var updated = _payments.Update(existing);
            return updated != null;
        }
    }
}
