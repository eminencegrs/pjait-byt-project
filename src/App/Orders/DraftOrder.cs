using System.Collections.Immutable;
using App.Addresses;

namespace App.Orders;

public class DraftOrder : OrderBase
{
    public Task<SubmittedOrder> Submit(IAddress address, PaymentInfo paymentInfo)
    {
        var order = new SubmittedOrder
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
