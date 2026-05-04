using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IOrderService
    {
        Task<int> PlaceOrderAsync(int userId);
        Task<IEnumerable<OrderModel>> GetUserOrderHistoryAsync(int userId);
    }
}
