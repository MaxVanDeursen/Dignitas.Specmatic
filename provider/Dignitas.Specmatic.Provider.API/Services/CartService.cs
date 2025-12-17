using Dignitas.Specmatic.Provider.API.Exceptions;
using Dignitas.Specmatic.Provider.API.Models;

namespace Dignitas.Specmatic.Provider.API.Services;

public class CartService
{
    private static readonly Dictionary<uint, Cart> Carts = new()
    {
        {
            0,
            new Cart
            {
                CartId = 0,
                UserId = 409,
                Items = [ new CartItem {
                    ProductId = 9,
                    Quantity = 1
                }]
            }
        }
    };
    private static uint _cartIdCounter = 1;

    private readonly UserService _userService;
    private readonly ProductService _productService;

    public CartService(UserService userService, ProductService productService)
    {
        _userService = userService;
        _productService = productService;
    }

    public InitializeCartResponse InitializeCart(InitializeCartRequest request)
    {
        if (!_userService.Exists(request.UserId))
        {
            throw new NotFoundException(NotFoundException.NotFoundReason.UserNotFound);

        }

        var cartId = _cartIdCounter++;
        var cart = new Cart
        {
            CartId = cartId,
            UserId = request.UserId,
            Items = new List<CartItem>()
        };

        Carts[cartId] = cart;

        return new InitializeCartResponse { CartId = cartId };
    }

    public void AddItemToCart(uint cartId, AddItemToCartRequest request)
    {
        if (!Carts.TryGetValue(cartId, out var cart))
        {
            throw new NotFoundException(NotFoundException.NotFoundReason.CartNotFound);
        }

        if (!_productService.Exists(request.ProductId))
        {
            throw new NotFoundException(NotFoundException.NotFoundReason.ProductNotFound);
        }

        if (cart.Items.Any(i => i.ProductId == request.ProductId))
        {
            throw new ConflictException($"Product {request.ProductId} already exists in cart");
        }

        cart.Items.Add(new CartItem
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity
        });
    }

    public Cart GetCart(uint cartId)
    {
        if (!Carts.TryGetValue(cartId, out var cart))
        {
            throw new NotFoundException(NotFoundException.NotFoundReason.CartNotFound);
        }
        return cart;
    }
}
