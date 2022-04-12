using Store.API.Models;

namespace Store.API.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProducts(int pageSize, int pageNumber);
        Task<ProductDto?> GetProductById(int id);
        Task<ProductDto?> CreateProductAsync(ProductDto product);
        Task<bool> ChangeProductPrice(int productId, int newPrice);
        Task<bool> ChangeProductQty(int productId, int newQty);
        Task<bool> DeleteProductById(int id);
        Task<bool> SaveChangesAsync();
    }
}
