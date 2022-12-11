using App.Addresses;
using App.Orders;

namespace App;

public class Account
{
    private readonly Dictionary<string, PaymentInfo> paymentInfos;
    
    private readonly Dictionary<string, IAddress> deliveryAddresses;
    
    private readonly Dictionary<string, IOrder> orderHistory;
    
    private readonly Dictionary<string, Product> favorites;
    
    public Account()
    {
        this.Id = Guid.NewGuid().ToString();
        this.paymentInfos = new Dictionary<string, PaymentInfo>();
        this.deliveryAddresses = new Dictionary<string, IAddress>();
        this.orderHistory = new Dictionary<string, IOrder>();
        this.favorites = new Dictionary<string, Product>();
    }

    public Account(
        IReadOnlyDictionary<string, PaymentInfo> paymentInfos,
        IReadOnlyDictionary<string, IAddress> deliveryAddresses,
        IReadOnlyDictionary<string, IOrder> orderHistory,
        IReadOnlyDictionary<string, Product> favorites)
    {
        this.Id = Guid.NewGuid().ToString();
        this.paymentInfos = paymentInfos.ToDictionary(
            k => k.Key,
            v => v.Value);
        this.deliveryAddresses = deliveryAddresses.ToDictionary(
            k => k.Key,
            v => v.Value);
        this.orderHistory = orderHistory.ToDictionary(
            k => k.Key,
            v => v.Value);
        this.favorites = favorites.ToDictionary(
            k => k.Key,
            v => v.Value);
    }
    
    public string Id { get; init; }

    public Task<bool> Create()
    {
        return Task.FromResult(true);
    }
    
    public Task<bool> Delete()
    {
        return Task.FromResult(true);
    }

    public Task<bool> AddPaymentMethod(PaymentInfo paymentInfo)
    {
        paymentInfo = paymentInfo ?? throw new ArgumentNullException(nameof(paymentInfo));

        return Task.FromResult(this.paymentInfos.TryAdd(paymentInfo.Id, paymentInfo));
    }

    public Task<bool> RemovePaymentMethod(PaymentInfo paymentInfo)
    {
        paymentInfo = paymentInfo ?? throw new ArgumentNullException(nameof(paymentInfo));

        return Task.FromResult(this.paymentInfos.Remove(paymentInfo.Id));
    }
    
    public Task<bool> AddDeliveryAddress(IAddress address)
    {
        address = address ?? throw new ArgumentNullException(nameof(address));

        return Task.FromResult(this.deliveryAddresses.TryAdd(address.Id, address));
    }

    public Task<bool> RemoveDeliveryAddress(IAddress address)
    {
        address = address ?? throw new ArgumentNullException(nameof(address));

        return Task.FromResult(this.deliveryAddresses.Remove(address.Id));
    }
    
    public Task<bool> UpdateDeliveryAddress(IAddress address)
    {
        address = address ?? throw new ArgumentNullException(nameof(address));

        if (this.deliveryAddresses.TryGetValue(address.Id, out var existing))
        {
            this.deliveryAddresses.Remove(address.Id);
        }
        
        return this.AddDeliveryAddress(address); 
    }
}
