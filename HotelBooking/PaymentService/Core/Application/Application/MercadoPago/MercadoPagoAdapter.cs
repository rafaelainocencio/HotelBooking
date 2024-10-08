using Application;
using Application.Payment;
using Application.Payment.Dtos;
using Application.Payment.Ports;
using Application.Payment.Responses;
using Payment.Application.MercadoPago.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.MercadoPago
{
    public class MercadoPagoAdapter : IMercadoPagoPaymentService
    {
        public Task<PaymentResponse> PayWithBankTransfer(string paymentIntention)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentResponse> PayWithCreditCart(string paymentIntention)
        {
            try
            {
                if (string.IsNullOrEmpty(paymentIntention))
                {
                    throw new InvalidPaymentIntentionException();
                }
                paymentIntention += "/success";

                var dto = new PaymentStateDto
                {
                    CreatedDate = DateTime.Now,
                    Message = $"Successfuly paid {paymentIntention}.",
                    PaymentId = "123",
                    Status = Status.Success,
                };

                var response = new PaymentResponse
                {
                    Success = true,
                    Data = dto,
                    Message = "Payment was successful.",
                };

                return Task.FromResult(response);
            }
            catch (InvalidPaymentIntentionException ex)
            {
                var resp = new PaymentResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.InvalidPaymentIntention,
                    Message = ex.Message,
                };
                return Task.FromResult(resp);
            }
        }

        public Task<PaymentResponse> PayWithDebitCart(string paymentIntention)
        {
            throw new NotImplementedException();
        }
    }
}
