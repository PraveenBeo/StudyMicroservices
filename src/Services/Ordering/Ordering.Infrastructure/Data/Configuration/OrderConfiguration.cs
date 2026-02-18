using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).HasConversion(
                orderId => orderId.Value,
                dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            builder.ComplexProperty(
                o => o.OrderName, namebuilder =>
                {
                    namebuilder.Property(n => n.Value).HasColumnName(nameof(Order.OrderName))
                        .HasMaxLength(100)
                        .IsRequired();
                });

            builder.ComplexProperty(
                o => o.ShippingAddress, addressBuilder =>
                {
                    addressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                    addressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                    addressBuilder.Property(a => a.Street).HasMaxLength(200);
                    addressBuilder.Property(a => a.City).HasMaxLength(100);
                    addressBuilder.Property(a => a.State).HasMaxLength(100);
                    addressBuilder.Property(a => a.PostalCode).HasMaxLength(2).IsRequired();
                    addressBuilder.Property(a => a.Country).HasMaxLength(100);
                });

            builder.ComplexProperty(o => o.BillingAddress, ab =>
            {
                ab.Property(a => a.FirstName).HasColumnName("BillingFirstName").HasMaxLength(50).IsRequired();
                ab.Property(a => a.LastName).HasColumnName("BillingLastName").HasMaxLength(50).IsRequired();
                ab.Property(a => a.Street).HasColumnName("BillingStreet").HasMaxLength(200);
                ab.Property(a => a.City).HasColumnName("BillingCity").HasMaxLength(100);
                ab.Property(a => a.State).HasColumnName("BillingState").HasMaxLength(100);
                ab.Property(a => a.PostalCode).HasColumnName("BillingPostalCode").HasMaxLength(20).IsRequired();
                ab.Property(a => a.Country).HasColumnName("BillingCountry").HasMaxLength(100);
            });
            builder.ComplexProperty(
                o => o.Payment,PaymentBuilder =>
                {
                    PaymentBuilder.Property(p => p.CardNumber).HasMaxLength(20).IsRequired();
                    PaymentBuilder.Property(p => p.CardHolderName).HasMaxLength(100);
                    PaymentBuilder.Property(p => p.ExpirationDate).IsRequired();
                    PaymentBuilder.Property(p => p.CVV).HasMaxLength(4).IsRequired();
                });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Pending)
                .HasConversion(
                    s => s.ToString(),
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

        }
    }
}
