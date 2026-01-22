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
        private readonly ProductDbContext productDbContext;
        public ProductRepository(ProductDbContext productDbContext)
        {
            this.productDbContext = productDbContext;

        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            return await productDbContext.Products.ToListAsync();
        }

    }
}
