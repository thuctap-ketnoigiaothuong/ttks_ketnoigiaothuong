using AutoMapper;
using dotnet9_ketnoigiaothuong.Domain.Entities;
using dotnet9_ketnoigiaothuong.Infrastructure.Context;
using dotnet9_ketnoigiaothuong.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.ProductContract;

namespace dotnet9_ketnoigiaothuong.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResponseProductModel>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Company)
                .Include(p => p.Category)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ResponseProductModel>>(products);
        }

        public async Task<ProductDetailModel> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Company)
                .Include(p => p.Category)
                .Include(p => p.ApprovedByUser)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
                return null;

            return _mapper.Map<ProductDetailModel>(product);
        }

        public async Task<ApiResponse<ResponseProductModel>> CreateProductAsync(CreateProductModel model, int currentUserId)
        {
            try
            {
                var company = await _context.Companies.FindAsync(model.CompanyID);
                if (company == null)
                    return new ApiResponse<ResponseProductModel>(false, "Công ty không tồn tại", null);

                var category = await _context.Categories.FindAsync(model.CategoryID);
                if (category == null)
                    return new ApiResponse<ResponseProductModel>(false, "Danh mục không tồn tại", null);
                
                var product = _mapper.Map<Product>(model);
                
                product.Status = "Pending"; 
                product.CreatedDate = DateTime.Now;

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                var createdProduct = await _context.Products
                    .Include(p => p.Company)
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.ProductID == product.ProductID);
                
                var responseModel = _mapper.Map<ResponseProductModel>(createdProduct);
                
                return new ApiResponse<ResponseProductModel>(true, "Sản phẩm đã được tạo thành công", responseModel);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ResponseProductModel>(false, $"Lỗi khi tạo sản phẩm: {ex.Message}", null);
            }
        }

        public async Task<ApiResponse<ResponseProductModel>> UpdateProductAsync(int id, UpdateProductModel model)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                    return new ApiResponse<ResponseProductModel>(false, "Không tìm thấy sản phẩm", null);
                

                if (model.CompanyID.HasValue)
                {
                    var company = await _context.Companies.FindAsync(model.CompanyID);
                    if (company == null)
                        return new ApiResponse<ResponseProductModel>(false, "Công ty không tồn tại", null);
                }
                
                if (model.CategoryID.HasValue)
                {
                    var category = await _context.Categories.FindAsync(model.CategoryID);
                    if (category == null)
                        return new ApiResponse<ResponseProductModel>(false, "Danh mục không tồn tại", null);
                }
                

                _mapper.Map(model, product);
                
                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                var updatedProduct = await _context.Products
                    .Include(p => p.Company)
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.ProductID == id);
                
                var responseModel = _mapper.Map<ResponseProductModel>(updatedProduct);
                
                return new ApiResponse<ResponseProductModel>(true, "Sản phẩm đã được cập nhật thành công", responseModel);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ResponseProductModel>(false, $"Lỗi khi cập nhật sản phẩm: {ex.Message}", null);
            }
        }

        public async Task<ApiResponse<bool>> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                    return new ApiResponse<bool>(false, "Không tìm thấy sản phẩm", false);
                

                var hasRelatedQuotations = await _context.QuotationRequests.AnyAsync(q => q.ProductID == id);
                if (hasRelatedQuotations)
                    return new ApiResponse<bool>(false, "Không thể xóa sản phẩm vì có yêu cầu báo giá liên quan", false);
                
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                
                return new ApiResponse<bool>(true, "Sản phẩm đã được xóa thành công", true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(false, $"Lỗi khi xóa sản phẩm: {ex.Message}", false);
            }
        }
    }
} 