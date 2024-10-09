using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payment.Dtos
{
    public enum SuportedPaymenrtProviders
    {
        Paypal = 1,
        Stripe = 2,
        PagSeguro = 3,
        MercadoPago = 4
    }

    public enum SupportedPaymenrMethods
    {
        DebitCard = 1,
        CreditCard = 2,
        BankTransfer = 3
    }

    public class PaymentRequestDto
    {
        public int BookingId { get; set; }
        public string PaymentIntention { get; set; }
        public SuportedPaymenrtProviders SelectedPaymentProvider { get; set; }
        public SupportedPaymenrMethods SelectedPaymentMethod { get; set; }
    }
}
