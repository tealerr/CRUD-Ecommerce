using System;
using System.Collections.Generic;

namespace Common.Repositories
{
    public class CartRepositories
    {
        public void AddItemToCart(int cartId, string itemId, int quantity)
        {
            // Implementation for adding item to cart
        }

        public void RemoveItemFromCart(int cartId, int itemId)
        {
            // Implementation for removing item from cart
        }

        public List<int> GetItemsInCart(int cartId)
        {
            // Implementation for retrieving items in cart
            return new List<int>();
        }
    }
}
