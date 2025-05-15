using dotnet9_ketnoigiaothuong.Domain.Contracts;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.CategoryContract;

namespace dotnet9_ketnoigiaothuong.Services.Category
{
    public interface ICategoryService
    {
        Task<ApiResponse<List<CategoryListItem>>> GetAllCategoriesAsync();
        Task<ApiResponse<CategoryDetailModel>> GetCategoryByIdAsync(int id);
        Task<ApiResponse<CategoryDetailModel>> CreateCategoryAsync(CreateCategoryModel model);
        Task<ApiResponse<CategoryDetailModel>> UpdateCategoryAsync(int id, UpdateCategoryModel model);
        Task<ApiResponse<bool>> DeleteCategoryAsync(int id);
    }
} 