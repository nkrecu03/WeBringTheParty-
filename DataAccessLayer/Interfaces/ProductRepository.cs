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
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext appDbContext;
        public ProductRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;

        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            return await appDbContext.Products.ToListAsync();
        }

        public async Task CreateProductAsync(ProductModel product)
        {
            await appDbContext.Products.AddAsync(product);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            return await appDbContext.Products.FindAsync(id);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await appDbContext.Products.FindAsync(id);
            if (product != null)
            {
                appDbContext.Products.Remove(product);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task EditProductAsync(ProductModel product)
        {
            appDbContext.Products.Update(product);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductModel>> GetBestSellersAsync(int count)
        {
            // Step 1 — get the top product IDs by units sold (runs in SQL)
            var topProductIds = await appDbContext.OrderItems
                .GroupBy(oi => oi.ProductId)
                .OrderByDescending(g => g.Sum(oi => oi.Quantity))
                .Take(count)
                .Select(g => g.Key)
                .ToListAsync();

            // Step 2 — fetch the actual products by those IDs (runs in SQL)
            var products = await appDbContext.Products
                .Where(p => topProductIds.Contains(p.ProductID) && p.isActive)
                .ToListAsync();

            // Step 3 — re-order in memory to match the sales rank order
            return topProductIds
                .Select(id => products.FirstOrDefault(p => p.ProductID == id))
                .Where(p => p != null)
                .ToList();
        }

    }
}
