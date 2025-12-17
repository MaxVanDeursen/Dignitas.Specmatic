using Dignitas.Specmatic.Provider.API.Models;
using Dignitas.Specmatic.Provider.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dignitas.Specmatic.Provider.API.Controllers;

[ApiController]
[Route("carts")]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost]
    public IActionResult InitializeCart([FromBody] InitializeCartRequest request)
    {
        var response = _cartService.InitializeCart(request);
        return CreatedAtAction(nameof(GetCart), new { cartId = response.CartId }, response);
    }

    [HttpPost("{cartId}/items")]
    public IActionResult AddItemToCart(uint cartId, [FromBody] AddItemToCartRequest request)
    {
        _cartService.AddItemToCart(cartId, request);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet("{cartId}")]
    public IActionResult GetCart(uint cartId)
    {
        return Ok(_cartService.GetCart(cartId));
    }
}
