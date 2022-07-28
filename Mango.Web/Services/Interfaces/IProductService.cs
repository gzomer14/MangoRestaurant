using Mango.Web.Models.DTO;

namespace Mango.Web.Services.Interfaces
{
    public interface IProductService : IBaseService
    {
        Task<T> GetAllProductsAsync<T>();
        Task<T> GetProductByIdAsync<T>(int id);
        Task<T> CreateProductAsync<T>(ProductDTO model);
        Task<T> UpdateProductAsync<T>(ProductDTO model);
        Task<T> DeleteProductAsync<T>(int id);

    }
}
