import axios from 'axios';
import { AddItemToCartRequest } from '../entities/add-item-to-cart-request';
import { AddItemToCartNotFoundResponse } from '../entities/add-item-to-cart-notfound-response';
import { Cart } from '../entities/cart';
import { InitializeCartRequest } from '../entities/initialize-cart-request';
import { InitializeCartResponse } from '../entities/initialize-cart-response';

export class CartService {
    readonly baseUrl: string;

    constructor(baseUrl: string = 'http://localhost:9000') {
        this.baseUrl = baseUrl;
    }

    async initializeCart(request: InitializeCartRequest): Promise<InitializeCartResponse> {
        try {
            const { data } = await axios.post<InitializeCartResponse>(`${this.baseUrl}/carts`, request);
            return data;
        } catch (error) {
            if (axios.isAxiosError(error)) {
                throw new Error(`Failed to initialize cart: ${error.response?.status}`);
            }
            throw error;
        }
    }

    async addItemToCart(cartId: number, request: AddItemToCartRequest): Promise<void | AddItemToCartNotFoundResponse> {
        try {
            await axios.post(`${this.baseUrl}/carts/${cartId}/items`, request);
        } catch (error) {
            if (axios.isAxiosError(error)) {
                if (error.response?.status === 404) {
                    return error.response.data as AddItemToCartNotFoundResponse;
                }
                if (error.response?.status === 409) {
                    throw new Error('Item already in cart');
                }
                throw new Error(`Failed to add item to cart: ${error.response?.status}`);
            }
            throw error;
        }
    }

    async getCart(cartId: number): Promise<Cart> {
        try {
            const { data } = await axios.get<Cart>(`${this.baseUrl}/carts/${cartId}`);
            return data;
        } catch (error) {
            if (axios.isAxiosError(error)) {
                throw new Error('Cart not found');
            }
            throw error;
        }
    }
}