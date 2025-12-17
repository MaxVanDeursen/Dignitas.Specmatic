namespace Dignitas.Specmatic.Provider.API.Services;

public class ProductService
{
    private static readonly HashSet<uint> Products = [.. Enumerable.Range(0, 10).Select(i => (uint)i)];

    public bool Exists(uint productId)
    {
        return Products.Contains(productId);
    }
}
