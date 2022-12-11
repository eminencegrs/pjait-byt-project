namespace App.Exceptions;

public class ProductAlreadyExistsException : Exception
{
    public ProductAlreadyExistsException(string id, string productName)
    {
        this.Id = id;
        this.ProductName = productName;
    }

    public string Id { get; }
    
    public string ProductName { get; }
}
