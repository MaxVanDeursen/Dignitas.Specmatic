namespace Dignitas.Specmatic.Provider.API.Models;

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

public class AddItemToCartNotFoundResponse: ProblemDetails
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public NotFoundReason Reason { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NotFoundReason
{
    CartNotFound,
    ProductNotFound,
    UserNotFound
}
