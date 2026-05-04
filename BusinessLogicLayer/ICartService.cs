using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface ICartService
    {
        Task AddToCartAsync(int userId, int productId, int quantity);
        Task<decimal> GetCartTotalAsync(int userId);
        Task RemoveFromCartAsync(int userID, int cartItemId);
        Task<IEnumerable<CartItemModel>> GetCartByUserIdAsync(int userId);
        Task AddItemToCartAsync(int userId, int productId, int quantity);



    }
}
