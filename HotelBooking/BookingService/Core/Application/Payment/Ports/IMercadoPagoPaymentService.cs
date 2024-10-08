using Application.Payment.Responses;

namespace Application.Payment.Ports
{
    public interface IMercadoPagoPaymentService
    {
        Task<PaymentResponse> PayWithCreditCart(string paymentIntention);
        Task<PaymentResponse> PayWithDebitCart(string paymentIntention);
        Task<PaymentResponse> PayWithBankTransfer(string paymentIntention);
    }
}
