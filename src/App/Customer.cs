namespace App;

public class Customer : User
{
    private readonly IReadOnlyDictionary<string, Account> accounts;
    
    public Customer(
        string firstName, 
        string lastName, 
        string email, 
        string passwordHash)
        : this(firstName, lastName, email, passwordHash, new Dictionary<string, Account>())
    {
    }
    
    public Customer(
        string firstName, 
        string lastName, 
        string email, 
        string passwordHash,
        IReadOnlyDictionary<string, Account> accounts)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.PasswordHash = passwordHash;
        this.Token = string.Empty;
        this.accounts = accounts.ToDictionary(k => k.Key, v => v.Value);
    }
    
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public string Email { get; init; }
    
    public string PasswordHash { get; init; }

    public string Token { get; private set; }

    public Task<bool> Logout()
    {
        this.Token = string.Empty;
        return Task.FromResult(true);
    }
}
