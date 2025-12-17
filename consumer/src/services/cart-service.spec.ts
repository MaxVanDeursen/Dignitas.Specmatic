import { describe, it, expect, inject } from 'vitest';
import { CartService } from './cart-service';

describe('CartService', () => {
    const port: number = +inject('port');
    let service = new CartService(`http://localhost:${port}`);

    it('should initialize a cart for a valid user', async () => {
        const request = { userId: 1 };

        const response = await service.initializeCart(request);

        expect(response).toBeDefined();
        expect(response.cartId).toBe(2);
    });

    it('should throw error when initializing cart for a non-existent user', async () => {
        const request = { userId: 404 };

        await expect(service.initializeCart(request))
            .rejects.toThrow('Failed to initialize cart: 404');
    });

    it('should get a cart for a valid cartId', async () => {
        const cart = await service.getCart(2);

        expect(cart.cartId).toBe(2);
        expect(cart.userId).toBe(1);
        expect(cart.items).toEqual([{productId: 3, quantity: 4}]);
    });

    it('should throw error when getting a cart for a non-existent cartId', async () => {
        await expect(service.getCart(404)).rejects.toThrow('Cart not found');
    });

    it('should add an item to a valid cart', async () => {
        await expect(service.addItemToCart(2, {
            productId: 3,
            quantity: 1
        })).resolves.toBeUndefined();
    });

    it('should return not found response when adding item to a non-existent cart', async () => {
        const request = { productId: 3, quantity: 1 };

        const result = await service.addItemToCart(404, request);

        expect(result).toHaveProperty('reason', 'CartNotFound');
    });

    it('should throw error when adding an item that already exists in the cart', async () => {
        await expect(service.addItemToCart(409, {
            productId: 3,
            quantity: 1
        })).rejects.toThrow('Item already in cart');
    });
});
