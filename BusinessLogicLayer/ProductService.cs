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

        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            return await productRepository.GetProductByIdAsync(id);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return false;
            }

            await productRepository.DeleteProductAsync(id);
            return true;
        }

        public async Task EditProductAsync(ProductModel product)
        {
            await productRepository.EditProductAsync(product);
        }

        public async Task<IEnumerable<ProductModel>> GetBestSellersAsync(int count)
        {
            return await productRepository.GetBestSellersAsync(count);
        }
    }
}
