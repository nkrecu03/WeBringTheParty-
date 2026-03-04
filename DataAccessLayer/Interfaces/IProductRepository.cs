using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetProductsAsync();
        Task CreateProductAsync(ProductModel product);
        Task<ProductModel> GetProductByIdAsync(int ProductID);
        Task DeleteProductAsync(int ProductID);
        Task EditProductAsync(ProductModel product);
    }
}
