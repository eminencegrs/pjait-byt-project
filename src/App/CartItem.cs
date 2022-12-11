namespace App;

// TODO: CartItem cam be a private internal class of the Cart one.
public class CartItem
{
    public string Id => this.Product.Id;
    
    public Product Product { get; init; }
    
    public int Amount { get; init; }

    public static Task<CartItem> Create(Product product, int amount)
    {
        product = product ?? throw new ArgumentNullException(nameof(product));

        // TODO: refactoring.
        return Task.FromResult(new CartItem
        {
            Product = product,
            Amount = amount
        });
    }
}
