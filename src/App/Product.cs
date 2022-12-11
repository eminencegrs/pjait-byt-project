namespace App;

public class Product
{
    public string Id { get; init; }
    
    public string Name { get; init; }

    public string Description { get; init; }
    
    public double? Price { get; init; }
    
    public Category? Category { get; init; }

    public static Task<Product> Create(
        string name,
        string description,
        double? price = null,
        Category? category = null)
    {
        var newProduct = new Product
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Description = description,
            Price = price,
            Category = category
        };

        if (category != null)
        {
            // TODO: implement
            category.AddProduct(newProduct);
        }

        return Task.FromResult(newProduct);
    }
    
    public Task<Product> Update(
        string? name = null,
        string? description = null,
        double? price = null,
        Category? category = null)
    {
        var product = new Product
        {
            Id = this.Id,
            Name = name ?? this.Name,
            Description = description ?? this.Description,
            Price = price ?? this.Price,
            Category = category ?? this.Category
        };

        if (this.Category != null)
        {
            // TODO:
            this.Category.RemoveProduct(this.Id);
        }

        if (category != null)
        {
            category.AddProduct(product);
        }

        return Task.FromResult(product);
    }
    
    public Task<bool> Delete()
    {
        // TODO: refactoring.
        return this.Category?.RemoveProduct(this.Id);
    }

    public Task<bool> AddTo(Cart cart)
    {
        cart = cart ?? throw new ArgumentNullException(nameof(cart));

        return cart.AddItem(this);
    }
    
    public Task<bool> RemoveFrom(Cart cart)
    {
        cart = cart ?? throw new ArgumentNullException(nameof(cart));

        return cart.RemoveItem(this.Id);
    }

    public sealed override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + this.Id.GetHashCode();
            hash = hash * 23 + this.Name.GetHashCode();
            hash = hash * 23 + this.Description.GetHashCode();
            hash = hash * 23 + this.Price?.GetHashCode() ?? 0;
            hash = hash * 23 + this.Category?.GetHashCode() ?? 0;
            return hash;
        }
    }
}
