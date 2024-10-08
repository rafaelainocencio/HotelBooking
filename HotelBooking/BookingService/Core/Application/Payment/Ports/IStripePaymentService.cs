using Application.Payment.Dtos;

namespace Application.Payment.Ports
{
    public interface IStripePaymentService
    {
        Task<PaymentStateDto> PayWithCreditCart(string paymentIntention);
        Task<PaymentStateDto> PayWithDebitCart(string paymentIntention);
        Task<PaymentStateDto> PayWithBankTransfer(string paymentIntention);
    }
}
