using AZ204.WebApp.Models;

namespace AZ204.WebApp.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAzync();
    }
}
