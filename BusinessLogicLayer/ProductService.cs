using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;

        }
        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            return await productRepository.GetProductsAsync();
        }

        public async Task CreateProductAsync(ProductModel product)
        {
            await productRepository.CreateProductAsync(product);
        }
    }
}
