namespace App.Addresses;

public abstract class AddressBase : IAddress
{
    private static IAddress empty;
    
    public string Id { get; init; }
    
    public string Building { get; init; }
    
    public string Street { get; init; }
    
    public string City { get; init; }
    
    public string Country { get; init; }
    
    public string Index { get; init; }
    
    public bool IsValid => this.Validate();

    public static IAddress Empty => empty ?? CreateEmpty();

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + this.Id.GetHashCode();
            hash = hash * 23 + this.Building.GetHashCode();
            hash = hash * 23 + this.Street.GetHashCode();
            hash = hash * 23 + this.City.GetHashCode();
            hash = hash * 23 + this.Country.GetHashCode();
            hash = hash * 23 + this.Index.GetHashCode();
            return hash;
        }
    }
    
    protected bool Validate()
    {
        return !(string.IsNullOrWhiteSpace(this.Id) ||
                 string.IsNullOrWhiteSpace(this.Building) ||
                 string.IsNullOrWhiteSpace(this.Street) ||
                 string.IsNullOrWhiteSpace(this.City) ||
                 string.IsNullOrWhiteSpace(this.Country) ||
                 string.IsNullOrWhiteSpace(this.Index));
    }

    private static IAddress CreateEmpty()
    {
        empty = new EmptyAddress
        {
            Id = string.Empty,
            Building = string.Empty,
            Street = string.Empty,
            City = string.Empty,
            Country = string.Empty,
            Index = string.Empty,
        };

        return empty;
    }
}