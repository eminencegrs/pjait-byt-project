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
            UpdatedAt = DateTime.UtcNow
        });
    }
}
