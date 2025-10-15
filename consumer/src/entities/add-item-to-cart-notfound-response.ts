export type AddItemToCartNotFoundResponse = {
    reason: 'CartNotFound' | 'ProductNotFound' | 'UserNotFound';
};