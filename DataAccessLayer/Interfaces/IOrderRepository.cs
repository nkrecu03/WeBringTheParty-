using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(OrderModel order);
        Task<IEnumerable<OrderModel>> GetOrdersByUserIdAsync(int userId);
    }
}
