namespace App.Addresses;

public interface IAddress
{
    string Id { get; }
    string Building { get; }
    string Street { get; }
    string City { get; }
    string Country { get; }
    string Index { get; }
    bool IsValid { get; }
}