namespace App.Exceptions;

public class ProductDoesNotExistException : Exception
{
    public ProductDoesNotExistException(string productName)
    {
        this.ProductName = productName;
    }
    
    public string ProductName { get; }
}
