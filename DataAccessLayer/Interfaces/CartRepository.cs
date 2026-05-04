using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context) => _context = context;

        public async Task<CartItemModel> GetCartItemAsync(int userId, int productId)
        {
            return await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.CartItemID == productId && ci.Cart.UserID == userId);
        }

        public async Task AddItemAsync(int userId, int productId, int quantity)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserID == userId);
            if (cart == null)
            {
                cart = new CartModel { UserID = userId };
                await _context.Carts.AddAsync(cart);
                await _context.SaveChangesAsync();
            }
            var cartItem = new CartItemModel
            {
                CartID = cart.CartID,
                ProductID = productId,
                Quantity = quantity
            };
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuantityAsync(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CartItemModel>> GetCartByUserIdAsync(int userId)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.Cart.UserID == userId)
                .ToListAsync();
        }

        public async Task RemoveItemAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateCartAsync(int userId)
        {
            var newCart = new CartModel
            {
                UserID = userId,
 
            };
            _context.Carts.Add(newCart);
            await _context.SaveChangesAsync();
        }

        public async Task SaveNewCartItemAsync(CartItemModel item)
        {
            _context.CartItems.Add(item);
            await _context.SaveChangesAsync();
        }


    }
}
