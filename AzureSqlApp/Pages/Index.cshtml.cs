using Microsoft.AspNetCore.Mvc.RazorPages;
using AzureSqlApp.Models;
using AzureSqlApp.Services;

namespace AzureSqlApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> Products;

        public void OnGet()
        {
            var productService = new ProductService();
            Products = productService.GetProducts();
        }
    }
}