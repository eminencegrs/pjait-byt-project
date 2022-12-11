using System.Collections.Immutable;
using App.Addresses;

namespace App.Orders;

public interface IOrder
{
    string Id { get; init; }
    
    DateTime CreatedAt { get; }
    
    DateTime UpdatedAt { get; }
    
    IImmutableDictionary<string, OrderItem> Products { get; }
    
    IAddress Address { get; }
    
    PaymentInfo PaymentInfo { get; }
}
