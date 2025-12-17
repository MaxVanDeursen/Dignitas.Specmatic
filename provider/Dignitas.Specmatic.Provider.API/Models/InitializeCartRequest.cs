using System.ComponentModel.DataAnnotations;

namespace Dignitas.Specmatic.Provider.API.Models;

public class InitializeCartRequest
{
    [Required]
    public uint UserId { get; set; }
}
