import { CartItem } from "./cart-item";

export type Cart = {
    cartId: number;
    userId: number;
    items: CartItem[];
}