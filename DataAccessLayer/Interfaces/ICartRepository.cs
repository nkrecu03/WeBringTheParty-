using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICartRepository
    {
        Task<CartItemModel> GetCartItemAsync(int userId, int productId);
        Task AddItemAsync(int userId, int productId, int quantity);
        Task UpdateQuantityAsync(int cartItemId, int quantity);
        Task<IEnumerable<CartItemModel>> GetCartByUserIdAsync(int userId); 
        Task RemoveItemAsync(int cartItemId);

        Task CreateCartAsync(int userId);

    }
}
