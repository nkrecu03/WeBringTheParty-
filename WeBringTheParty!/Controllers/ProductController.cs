using BusinessLogicLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WeBringTheParty_.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productService.GetProductsAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel createdProduct)
        {
            await productService.CreateProductAsync(createdProduct);          

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShopAll()
        {
            var products = await productService.GetProductsAsync();
            return View(products);
        }
    }
}
