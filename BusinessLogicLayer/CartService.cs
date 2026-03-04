using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;

        public CartService(ICartRepository cartRepo, IProductRepository productRepo)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }

        public async Task AddToCartAsync(int userId, int productId, int quantity)
        {
            // 1. Business Rule: Check if product exists/has stock
            var product = await _productRepo.GetProductByIdAsync(productId);
            if (product == null || product.QuantityAvailable < quantity) throw new Exception("Out of stock!");

            // 2. Business Rule: If item exists in cart, just update quantity
            var existingItem = await _cartRepo.GetCartItemAsync(userId, productId);

            if (existingItem != null)
            {
                await _cartRepo.UpdateQuantityAsync(existingItem.ProductID, existingItem.Quantity + quantity);
            }
            else
            {
                await _cartRepo.AddItemAsync(userId, productId, quantity);
            }
        }
        public async Task<decimal> GetCartTotalAsync(int userId)
        {
            var items = await _cartRepo.GetCartByUserIdAsync(userId);
            return items.Sum(i => i.Product.Price * i.Quantity);
        }

        public async Task RemoveFromCartAsync(int userId, int cartItemId)
        {
            var item = await _cartRepo.GetCartItemAsync(userId, cartItemId);

            // We assume your CartItem entity has a navigation property back to the Cart/UserId
            if (item == null || item.Cart.UserID != userId)
            {
                throw new UnauthorizedAccessException("Cannot remove an item that isn't in your cart.");
            }

            // 2. Perform the removal
            await _cartRepo.RemoveItemAsync(cartItemId);
        }
        public async Task<IEnumerable<CartItemModel>> GetCartByUserIdAsync(int userId)
        {
            // Call the DAL to get the raw data
            var items = await _cartRepo.GetCartByUserIdAsync(userId);

            // If you need to do any calculations (like discounts) before showing 
            // the user their cart, you would do that here.

            return items;
        }

    }
}
