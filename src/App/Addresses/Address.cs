namespace App.Addresses;

public class Address : AddressBase
{
    public Task<bool> AddTo(Account account)
    {
        account = account ?? throw new ArgumentNullException(nameof(account));

        return account.AddDeliveryAddress(this);
    }
    
    public Task<bool> RemoveFrom(Account account)
    {
        account = account ?? throw new ArgumentNullException(nameof(account));

        return account.RemoveDeliveryAddress(this);
    }
    
    public Task<bool> Update(Account account)
    {
        account = account ?? throw new ArgumentNullException(nameof(account));

        return account.UpdateDeliveryAddress(this);
    }
}
