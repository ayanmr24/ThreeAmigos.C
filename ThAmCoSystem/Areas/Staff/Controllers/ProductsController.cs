using Microsoft.AspNetCore.Mvc;
using ThAmCoSystem.Areas.Identity.Data;
using ThAmCoSystem.Models.Products;

namespace ThAmCoSystem.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class ProductsController : Controller
    {

        private readonly ThAmCoSystemContext _context;

        public ProductsController(ThAmCoSystemContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {

            return View();
        }



        public async Task<IActionResult> CreateProduct()
        {

            return View();
        }

 

        // POST: /Staff/Products/Product
        [HttpPost]
        public IActionResult Product(Product product, IFormFile file)
        {
            // Handle file upload if an image is selected
            if (file != null && file.Length > 0)
            {
                // Example: Save file to wwwroot/images/products
                var imagePath = "/images/products/" + file.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", file.FileName);

                // Create the directory if it does not exist
                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                product.ProducImageUrl = imagePath;
            }

            // Add the product to the database
            _context.Products.Add(product);
            _context.SaveChanges();

            // Set a temporary message
            TempData["SuccessMessage"] = "Product created successfully";

            return RedirectToAction("Index"); // Redirect to the desired action after successful submission
        }




        public JsonResult GetProuct()
        {
            var brands = _context.Products.ToList();

            return Json(new { data = brands });
        }




    }
}