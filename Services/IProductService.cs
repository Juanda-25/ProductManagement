using ProductManagement.DTOs;
using ProductManagement.Models;

namespace ProductManagement.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(ProductDto dto);
        Task<Product?> UpdateAsync(int id, ProductDto dto);
        Task<bool> DeleteAsync(int id);
        Task UpdateImageAsync(int id, string imageUrl);
    }
}