using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardNumber { get; init; }= default!;
        public string? CardHolderName { get; init; }= default!;
        public string ExpirationDate { get; init; }= default!;
        public string CVV { get; init; }= default!;
        public int PaymentMethod { get; init; } = default!;

        protected Payment() { }

        private Payment(string cardNumber, string? cardHolderName, string expirationDate, string cvv, int paymentMethod)
        {
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            ExpirationDate = expirationDate;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string cardNumber, string? cardHolderName, string expirationDate, string cvv, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(expirationDate);
            ArgumentException.ThrowIfNullOrWhiteSpace(cvv);

            return new Payment(cardNumber, cardHolderName, expirationDate, cvv, paymentMethod);
        }

    }
}
