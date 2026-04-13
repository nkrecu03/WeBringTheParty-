using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;

        public OrderService(IOrderRepository orderRepo, ICartRepository cartRepo, IProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }

        public async Task<int> PlaceOrderAsync(int userId)
        {
            var cartItems = await _cartRepo.GetCartByUserIdAsync(userId);
            if (!cartItems.Any()) throw new Exception("Cart is empty.");

            // 1. Inventory Update Logic & Validation
            foreach (var item in cartItems)
            {
                var product = await _productRepo.GetProductByIdAsync(item.ProductID);
                if (product.QuantityAvailable < item.Quantity)
                {
                    throw new Exception($"Not enough stock for {product.Name}.");
                }
                product.QuantityAvailable -= item.Quantity; //
                await _productRepo.EditProductAsync(product);
            }

            // 2. Save Order and OrderItems
            var order = new OrderModel
            {
                UserId = userId,
                Date = DateTime.Now,
                OrderItems = cartItems.Select(ci => new OrderItemModel
                {
                    ProductId = ci.ProductID,
                    Quantity = ci.Quantity
                }).ToList()
            };

            await _orderRepo.CreateOrderAsync(order);

            // 3. Clear Cart after checkout
            foreach (var item in cartItems)
            {
                await _cartRepo.RemoveItemAsync(item.CartItemID);
            }

            return order.OrderId;
        }

        public async Task<IEnumerable<OrderModel>> GetUserOrderHistoryAsync(int userId)
        {
            return await _orderRepo.GetOrdersByUserIdAsync(userId);
        }
    }
}
