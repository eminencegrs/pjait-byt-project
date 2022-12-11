namespace App;

public abstract class User
{
    public string Id { get; init; }

    public Task<bool> Login(string email, string passwordHash)
    {
        return Task.FromResult(true);
    }
    
    public Task<bool> Register(string email, string passwordHash)
    {
        return Task.FromResult(true);
    }
}
