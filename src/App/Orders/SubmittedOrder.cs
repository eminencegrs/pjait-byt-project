using System.Collections.Immutable;

namespace App.Orders;

public class SubmittedOrder : OrderBase
{
    public Task<CanceledOrder> Cancel()
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
    
    public Task<PaidOrder> Pay(PaymentInfo paymentInfo)
    {
        var order = new PaidOrder
        {
            Id = this.Id,
            CreatedAt = this.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
            PaymentInfo = paymentInfo,
            Products = this.Products.ToImmutableDictionary(
                k => k.Key,
                v => new OrderItem { Product = v.Value.Product, Amount = v.Value.Amount })
        };
        
        return Task.FromResult(order);
    }
}
