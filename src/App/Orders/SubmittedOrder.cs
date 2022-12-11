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
            Items = this.Items.ToImmutableDictionary(
                k => k.Key,
                v => new OrderItem { Product = v.Value.Product, Amount = v.Value.Amount }),
            Address = this.Address,
            PaymentInfo = this.PaymentInfo
        };
        
        return Task.FromResult(order);
    }
    
    public Task<PaidOrder> Pay()
    {
        var order = new PaidOrder
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
