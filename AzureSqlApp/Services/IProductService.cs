using AzureSqlApp.Models;

namespace AzureSqlApp.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<bool> IsBeta();
    }
}