namespace Dignitas.Specmatic.Provider.API.Models;

public class Cart
{
    public uint CartId { get; set; }
    public uint UserId { get; set; }
    public List<CartItem> Items { get; set; } = new();
}
