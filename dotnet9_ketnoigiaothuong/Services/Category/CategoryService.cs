using AutoMapper;
using dotnet9_ketnoigiaothuong.Domain.Contracts;
using dotnet9_ketnoigiaothuong.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.CategoryContract;

namespace dotnet9_ketnoigiaothuong.Services.Category
{
    public class CategoryService : BaseRepository, ICategoryService
    {
        public CategoryService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ApiResponse<List<CategoryListItem>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await context.Categories
                    .Include(c => c.ParentCategory)
                    .ToListAsync();

                var categoryList = mapper.Map<List<CategoryListItem>>(categories);

                return new ApiResponse<List<CategoryListItem>>
                {
                    Data = categoryList,
                    IsSuccess = true,
                    Message = "Lấy danh sách danh mục thành công"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CategoryListItem>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = $"Lỗi khi lấy danh sách danh mục: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<CategoryDetailModel>> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await context.Categories
                    .Include(c => c.ParentCategory)
                    .Include(c => c.SubCategories)
                    .FirstOrDefaultAsync(c => c.CategoryID == id);

                if (category == null)
                {
                    return new ApiResponse<CategoryDetailModel>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Không tìm thấy danh mục"
                    };
                }

                var categoryDetail = mapper.Map<CategoryDetailModel>(category);

                return new ApiResponse<CategoryDetailModel>
                {
                    Data = categoryDetail,
                    IsSuccess = true,
                    Message = "Lấy thông tin danh mục thành công"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryDetailModel>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = $"Lỗi khi lấy thông tin danh mục: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<CategoryDetailModel>> CreateCategoryAsync(CreateCategoryModel model)
        {
            try
            {
                // Kiểm tra xem parent category có tồn tại không
                if (model.ParentCategoryID.HasValue)
                {
                    var parentCategory = await context.Categories.FindAsync(model.ParentCategoryID.Value);
                    if (parentCategory == null)
                    {
                        return new ApiResponse<CategoryDetailModel>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "Danh mục cha không tồn tại"
                        };
                    }
                }

                // Kiểm tra tên danh mục đã tồn tại chưa
                var existingCategory = await context.Categories
                    .FirstOrDefaultAsync(c => c.CategoryName == model.CategoryName && c.ParentCategoryID == model.ParentCategoryID);
                if (existingCategory != null)
                {
                    return new ApiResponse<CategoryDetailModel>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Danh mục với tên này đã tồn tại trong cùng danh mục cha"
                    };
                }

                var category = mapper.Map<Domain.Entities.Category>(model);
                
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                // Lấy category mới tạo kèm parent để mapping
                var newCategory = await context.Categories
                    .Include(c => c.ParentCategory)
                    .Include(c => c.SubCategories)
                    .FirstOrDefaultAsync(c => c.CategoryID == category.CategoryID);

                var categoryDetail = mapper.Map<CategoryDetailModel>(newCategory);

                return new ApiResponse<CategoryDetailModel>
                {
                    Data = categoryDetail,
                    IsSuccess = true,
                    Message = "Tạo danh mục thành công"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryDetailModel>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = $"Lỗi khi tạo danh mục: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<CategoryDetailModel>> UpdateCategoryAsync(int id, UpdateCategoryModel model)
        {
            try
            {
                var category = await context.Categories.FindAsync(id);
                if (category == null)
                {
                    return new ApiResponse<CategoryDetailModel>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Không tìm thấy danh mục"
                    };
                }

                // Kiểm tra parent category có tồn tại không
                if (model.ParentCategoryID.HasValue)
                {
                    // Không cho phép đặt parent là chính nó
                    if (model.ParentCategoryID.Value == id)
                    {
                        return new ApiResponse<CategoryDetailModel>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "Không thể đặt danh mục cha là chính nó"
                        };
                    }

                    var parentCategory = await context.Categories.FindAsync(model.ParentCategoryID.Value);
                    if (parentCategory == null)
                    {
                        return new ApiResponse<CategoryDetailModel>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "Danh mục cha không tồn tại"
                        };
                    }

                    // Kiểm tra xem parent mới có phải là một trong các subcategory của nó không (tránh tạo vòng)
                    bool isSubCategory = await IsSubCategoryOf(model.ParentCategoryID.Value, id);
                    if (isSubCategory)
                    {
                        return new ApiResponse<CategoryDetailModel>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "Không thể đặt danh mục con làm danh mục cha"
                        };
                    }
                }

                // Kiểm tra tên danh mục đã tồn tại chưa
                var existingCategory = await context.Categories
                    .FirstOrDefaultAsync(c => c.CategoryName == model.CategoryName && 
                                             c.ParentCategoryID == model.ParentCategoryID && 
                                             c.CategoryID != id);
                if (existingCategory != null)
                {
                    return new ApiResponse<CategoryDetailModel>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Danh mục với tên này đã tồn tại trong cùng danh mục cha"
                    };
                }

                mapper.Map(model, category);
                
                context.Categories.Update(category);
                await context.SaveChangesAsync();

                // Lấy category đã cập nhật kèm parent để mapping
                var updatedCategory = await context.Categories
                    .Include(c => c.ParentCategory)
                    .Include(c => c.SubCategories)
                    .FirstOrDefaultAsync(c => c.CategoryID == id);

                var categoryDetail = mapper.Map<CategoryDetailModel>(updatedCategory);

                return new ApiResponse<CategoryDetailModel>
                {
                    Data = categoryDetail,
                    IsSuccess = true,
                    Message = "Cập nhật danh mục thành công"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryDetailModel>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = $"Lỗi khi cập nhật danh mục: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<bool>> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await context.Categories.FindAsync(id);
                if (category == null)
                {
                    return new ApiResponse<bool>
                    {
                        Data = false,
                        IsSuccess = false,
                        Message = "Không tìm thấy danh mục"
                    };
                }

                // Kiểm tra xem có danh mục con không
                bool hasSubCategories = await context.Categories.AnyAsync(c => c.ParentCategoryID == id);
                if (hasSubCategories)
                {
                    return new ApiResponse<bool>
                    {
                        Data = false,
                        IsSuccess = false,
                        Message = "Không thể xóa danh mục vì có danh mục con"
                    };
                }

                // Kiểm tra xem có sản phẩm nào thuộc danh mục này không
                bool hasProducts = await context.Products.AnyAsync(p => p.CategoryID == id);
                if (hasProducts)
                {
                    return new ApiResponse<bool>
                    {
                        Data = false,
                        IsSuccess = false,
                        Message = "Không thể xóa danh mục vì có sản phẩm liên kết"
                    };
                }

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Message = "Xóa danh mục thành công"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    Message = $"Lỗi khi xóa danh mục: {ex.Message}"
                };
            }
        }

        // Kiểm tra xem categoryId có phải là con cháu của parentId hay không
        private async Task<bool> IsSubCategoryOf(int categoryId, int parentId)
        {
            var category = await context.Categories.FindAsync(categoryId);
            if (category == null || !category.ParentCategoryID.HasValue)
                return false;

            if (category.ParentCategoryID.Value == parentId)
                return true;

            return await IsSubCategoryOf(category.ParentCategoryID.Value, parentId);
        }
    }
} 