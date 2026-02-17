using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; }
        public string Street { get; init; } = default!;
        public string City { get; init; } = default!;
        public string State { get; init; } = default!;
        public string PostalCode { get; init; } = default!;
        public string Country { get; init; } = default!;

        protected Address() { }
        private Address(string firstName, string lastName, string street, string city, string state, string postalCode, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }

        public static Address Of(string firstName, string lastName, string street, string city, string state, string postalCode, string country)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(street);
            ArgumentException.ThrowIfNullOrWhiteSpace(country);
            return new Address(firstName, lastName, street, city, state, postalCode, country);

        }
    }
}
