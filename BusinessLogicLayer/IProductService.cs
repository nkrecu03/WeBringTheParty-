using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetProductsAsync();
        Task CreateProductAsync(ProductModel product);
        Task<ProductModel> GetProductByIdAsync(int id);
        Task<bool> DeleteProductAsync(int id);
        Task EditProductAsync(ProductModel product);
    }
}
