using System.Collections.Immutable;
using App.Addresses;

namespace App.Orders;

public class PaidOrder : OrderBase
{
    public PaymentInfo PaymentInfo { get; init; }
    
    public Task<DeliveredOrder> Deliver()
    {
        var order = new DeliveredOrder
        {
            Id = this.Id,
            CreatedAt = this.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
            Items = this.Items.ToImmutableDictionary(
                k => k.Key,
                v => new OrderItem { Product = v.Value.Product, Amount = v.Value.Amount }),
            Address = this.Address,
            PaymentInfo = this.PaymentInfo
        };
        
        return Task.FromResult(order);
    }
    
    public Task<RefundedOrder> Refund()
    {
        var order = new RefundedOrder
        {
            Id = this.Id,
            CreatedAt = this.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
            Items = this.Items.ToImmutableDictionary(
                k => k.Key,
                v => new OrderItem { Product = v.Value.Product, Amount = v.Value.Amount }),
            Address = this.Address,
            PaymentInfo = this.PaymentInfo
        };
        
        return Task.FromResult(order);
    }
}
