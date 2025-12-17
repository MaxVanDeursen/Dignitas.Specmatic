using System.ComponentModel.DataAnnotations;

namespace Dignitas.Specmatic.Provider.API.Models;

public class AddItemToCartRequest
{
    [Required]
    public uint ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public uint Quantity { get; set; }
}
