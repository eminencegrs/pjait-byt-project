namespace App;

public class PaymentInfo
{
    public string Id { get; init; }
    
    public string CardNumber { get; init; }
    
    public DateTime ExpirationDate { get; init; }
    
    public string CvvHash { get; init; }

    public Task<bool> AddTo(Account account)
    {
        account = account ?? throw new ArgumentNullException(nameof(account));

        return account.AddPaymentMethod(this);
    }
    
    public Task<bool> RemoveFrom(Account account)
    {
        account = account ?? throw new ArgumentNullException(nameof(account));

        return account.RemovePaymentMethod(this);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + this.Id.GetHashCode();
            hash = hash * 23 + this.CardNumber.GetHashCode();
            return hash;
        }
    }
}
