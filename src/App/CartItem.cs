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

        if (amount < 1)
        {
            throw new ArgumentException("Amount must be equal to or grater than 1.", nameof(amount));
        }

        return Task.FromResult(new CartItem
        {
            Product = product,
            Amount = amount
        });
    }
}
