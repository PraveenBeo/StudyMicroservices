using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models
{
    public class OrderItem :Entity<OrderItemId>
    {
        internal OrderItem(OrderId orderId, ProductId productId, decimal price, int quantity)
        {
            Id = OrderItemId.Of(Guid.NewGuid());
            OrderId = orderId;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }
        public OrderId OrderId { get; private set; } = default!;
        public ProductId ProductId { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
        public int Quantity { get; private set; } = default!;
    }
}
