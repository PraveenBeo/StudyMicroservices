using Ordering.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int MaxLength = 5;
        public string Value { get; init; } = default!;

       private OrderName(string value) => Value = value;
        public static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, MaxLength);
           
            return new OrderName(value);
        }

    }
}
