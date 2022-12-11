using System.Collections.Immutable;

namespace App.Orders;

public class CanceledOrder : OrderBase
{
    public Task<ArchivedOrder> Archive()
    {
        var order = new ArchivedOrder
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
