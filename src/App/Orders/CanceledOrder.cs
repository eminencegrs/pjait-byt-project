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
            Products = this.Products.ToImmutableDictionary(
                k => k.Key,
                v => new OrderItem { Product = v.Value.Product, Amount = v.Value.Amount })
        };
        
        return Task.FromResult(order);
    }
}
