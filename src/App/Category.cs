using System.Collections.Immutable;
using App.Exceptions;

namespace App;

public class Category
{
    public string Id { get; init; }
    
    public string Name { get; init; }

    public string Description { get; init; }
    
    public IImmutableDictionary<string, Product> Products { get; private set; } = ImmutableDictionary<string, Product>.Empty;

    public sealed override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + this.Id.GetHashCode();
            hash = hash * 23 + this.Name.GetHashCode();
            hash = hash * 23 + this.Description.GetHashCode();
            return hash;
        }
    }

    public Task<bool> AddProduct(Product product)
    {
        product = product ?? throw new ArgumentNullException(nameof(product));

        if (this.Products.Contains(new KeyValuePair<string, Product>(product.Id, product)))
        {
            throw new ProductAlreadyExistsException(product.Id, product.Name);
        }

        this.Products = this.Products.Add(product.Id, product);

        return Task.FromResult(true);
    }
    
    public Task<bool> RemoveProduct(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException($"{nameof(id)} must not be null, empty or white-space.", nameof(id));
        }
        
        // TODO: check if a product exists.
        this.Products = this.Products.Remove(id);

        return Task.FromResult(true);
    }

    public Task<bool> Delete(Catalog catalog)
    {
        catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
        
        return catalog.RemoveCategory(this.Id);
    }

    public static Task<Category> Create(string name, string description, Catalog catalog)
    {
        catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));

        var category = new Category
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Description = description
        };

        // TODO: check if a category does not exist.
        catalog.AddCategory(category);

        return Task.FromResult(category);
    }
}