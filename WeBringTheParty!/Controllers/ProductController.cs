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

        [HttpGet]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("DeleteProduct")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await productService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel editedProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(editedProduct);
            }
            try
            {
                await productService.EditProductAsync(editedProduct);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(editedProduct);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel model, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // Ensure wwwroot/product_images exists
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product_images");
                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);

                    // Create a unique file name
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine(uploads, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    // Save the file name to the model
                    model.ImageUrl = fileName;
                }

                await productService.CreateProductAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
