using Microsoft.EntityFrameworkCore;
using Store.API.DataContext;
using Store.API.Entities;
using Store.API.Models;

namespace Store.API.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationContext _context;
        public ProductService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> ChangeProductPrice(int productId, int newPrice)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
                return false;

            product.Price = newPrice;
            return await SaveChangesAsync();
        }

        public async Task<bool> ChangeProductQty(int productId, int newQty)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
                return false;

            product.Quantity = newQty;
            return await SaveChangesAsync();
        }

        public async Task<ProductDto?> CreateProductAsync(ProductDto product)
        {
            var productToCreate = new Product()
            {
                ProductName = product.ProductName,
                Quantity = product.Quantity,
                Price = product.Price,
                BarCode = product.BarCode
            };
            await _context.Products.AddAsync(productToCreate);
            if (await SaveChangesAsync())
                return product;
            else
                return null;
        }

        public async Task<bool> DeleteProductById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            return await SaveChangesAsync();
        }

        public async Task<List<ProductDto>> GetAllProducts(int pageSize, int pageNumber)
        {
            return await _context.Products
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Select(p => new ProductDto
                {
                    ProductName = p.ProductName,
                    Price = p.Price,
                    BarCode = p.BarCode,
                    Quantity = p.Quantity
                }).ToListAsync();
        }

        public async Task<ProductDto?> GetProductById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return null;
            else
                return new ProductDto()
                {
                    ProductName = product.ProductName,
                    Price = product.Price,
                    BarCode = product.BarCode,
                    Quantity = product.Quantity
                };
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync())> 0;
        }
    }
}
