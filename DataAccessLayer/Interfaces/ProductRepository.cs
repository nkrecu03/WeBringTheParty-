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

    }
}
