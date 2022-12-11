using System.Collections.Immutable;
using App.Addresses;

namespace App.Orders;


public abstract class OrderBase : IOrder
{
    protected OrderBase()
    {
        this.Id = Guid.NewGuid().ToString();
        this.CreatedAt = DateTime.UtcNow;
        this.UpdatedAt = DateTime.UtcNow;
        this.Items = ImmutableDictionary<string, OrderItem>.Empty;
    }
    
    public string Id { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    public DateTime UpdatedAt { get; init; }
    
    public IImmutableDictionary<string, OrderItem> Items { get; init; }
    
    public IAddress Address { get; init; } = AddressBase.Empty;
    
    public PaymentInfo PaymentInfo { get; init; }
    
    public sealed override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + this.Id.GetHashCode();
            hash = hash * 23 + this.CreatedAt.GetHashCode();
            hash = hash * 23 + this.UpdatedAt.GetHashCode();
            hash = hash * 23 + this.Items.GetHashCode();
            return hash;
        }
    }

    public static Task<DraftOrder> Create(Cart cart)
    {
        var dateTime = DateTime.UtcNow;
        var order = new DraftOrder
        {
            Id = Guid.NewGuid().ToString(),
            CreatedAt = dateTime,
            UpdatedAt = dateTime,
            Items = cart.Items.ToImmutableDictionary(
                k => k.Key,
                v => new OrderItem { Product = v.Value.Product, Amount = v.Value.Amount })
        };

        return Task.FromResult(order);
    }
}
