using System.Collections.Immutable;

namespace App;

public class Cart
{
    public IImmutableDictionary<string, CartItem> Items { get; private set; } = ImmutableDictionary<string, CartItem>.Empty;

    public async Task<bool> AddItem(Product product)
    {
        product = product ?? throw new ArgumentNullException(nameof(product));
        
        var cartItem = this.Items.TryGetValue(product.Id, out CartItem? existingItem)
            ? await CartItem.Create(product, existingItem.Amount + 1)
            : await CartItem.Create(product, 1);

        this.Items = this.Items.SetItem(cartItem.Id, cartItem);

        return true;
    }
    
    public Task<bool> RemoveItem(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException($"{nameof(id)} must not be null, empty or white-space.", nameof(id));
        }
        
        this.Items = this.Items.Remove(id);
        
        return Task.FromResult(true);
    }
    
    public async Task<bool> ChangeAmount(string id, int amount)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException($"{nameof(id)} must not be null, empty or white-space.", nameof(id));
        }

        if (amount < 1)
        {
            throw new ArgumentException($"{nameof(amount)} must not be less than 1.", nameof(amount));
        }

        if (!this.Items.TryGetValue(id, out CartItem? existingItem))
        {
            return false;
        }
        
        var updatedItem = await CartItem.Create(existingItem.Product, existingItem.Amount + amount);
        this.Items = this.Items.SetItem(updatedItem.Id, updatedItem);
        return true;
    }
}
