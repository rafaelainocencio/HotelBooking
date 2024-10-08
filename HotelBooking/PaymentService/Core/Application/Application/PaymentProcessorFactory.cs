using Application.Payment.Dtos;
using Application.Payment.Ports;
using Payment.Application.MercadoPago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application
{
    public class PaymentProcessorFactory : IPaymentProcessorFactory
    {
        public IPaymentProcessor GetPaymentProcessor(SuportedPaymenrtProviders selectedPaymentProvider)
        {
            switch (selectedPaymentProvider)
            {
                case SuportedPaymenrtProviders.MercadoPago:
                    return new MercadoPagoAdapter();

                default:
                    return new NotImplementedPaymentProvider();
            }
        }
    }
}
