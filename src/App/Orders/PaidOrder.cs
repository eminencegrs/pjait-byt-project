using System.Collections.Immutable;
using App.Addresses;

namespace App.Orders;

public class PaidOrder : OrderBase
{
    public PaymentInfo PaymentInfo { get; init; }
    
    public Task<DeliveredOrder> Deliver(IAddress address)
    {
        var order = new DeliveredOrder
        {
            Id = this.Id,
            CreatedAt = this.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
            Address = address,
            Products = this.Products.ToImmutableDictionary(
                k => k.Key,
                v => new OrderItem { Product = v.Value.Product, Amount = v.Value.Amount })
        };
        
        return Task.FromResult(order);
    }
    
    public Task<CanceledOrder> Refund()
    {
        var order = new CanceledOrder
        {
            Id = this.Id,
            CreatedAt = this.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
            Products = this.Products.ToImmutableDictionary(
                k => k.Key,
                v => new OrderItem { Product = v.Value.Product, Amount = v.Value.Amount })
        };
        
        return Task.FromResult(order);
    }
}
