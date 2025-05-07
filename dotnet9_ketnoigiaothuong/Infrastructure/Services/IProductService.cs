using dotnet9_ketnoigiaothuong.Domain.Entities;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.ProductContract;

namespace dotnet9_ketnoigiaothuong.Infrastructure.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ResponseProductModel>> GetAllProductsAsync();
        Task<ProductDetailModel> GetProductByIdAsync(int id);
        Task<ApiResponse<ResponseProductModel>> CreateProductAsync(CreateProductModel model, int currentUserId);
        Task<ApiResponse<ResponseProductModel>> UpdateProductAsync(int id, UpdateProductModel model);
        Task<ApiResponse<bool>> DeleteProductAsync(int id);
    }
} 