using System.Collections.Immutable;
using App.Addresses;

namespace App.Orders;

public class DeliveredOrder : OrderBase
{
    public IAddress Address { get; init; } = AddressBase.Empty;
    
    public Task<ArchivedOrder> Archive()
    {
        return Task.FromResult(new ArchivedOrder
        {
            Id = this.Id,
            CreatedAt = this.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
            Items = this.Items.ToImmutableDictionary(
                k => k.Key,
                v => new OrderItem { Product = v.Value.Product, Amount = v.Value.Amount }),
            Address = this.Address,
            PaymentInfo = this.PaymentInfo
        });
    }
}
