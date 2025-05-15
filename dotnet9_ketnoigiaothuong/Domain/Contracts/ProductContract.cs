using dotnet9_ketnoigiaothuong.Domain.Entities;

namespace dotnet9_ketnoigiaothuong.Domain.Contracts
{
    public class ProductContract
    {
        public class ResponseProductModel
        {
            public int ProductID { get; set; }
            public string? ProductName { get; set; }
            public string? Description { get; set; }
            public double? UnitPrice { get; set; }
            public int? StockQuantity { get; set; }
            public string? Status { get; set; }
            public string? Image { get; set; }
            public DateTime? CreatedDate { get; set; }
            public string? CompanyName { get; set; }
            public string? CategoryName { get; set; }
        }

        public class ProductDetailModel
        {
            public int ProductID { get; set; }
            public string? ProductName { get; set; }
            public string? Description { get; set; }
            public double? UnitPrice { get; set; }
            public int? StockQuantity { get; set; }
            public string? Status { get; set; }
            public string? Image { get; set; }
            public DateTime? CreatedDate { get; set; }
            public string? ApprovalNotes { get; set; }
            
            public int? CompanyID { get; set; }
            public string? CompanyName { get; set; }
            
            public int? CategoryID { get; set; }
            public string? CategoryName { get; set; }
            
            public int? ApprovedBy { get; set; }
            public string? ApprovedByUserName { get; set; }
        }

        public class CreateProductModel
        {
            public int? CompanyID { get; set; }
            public int? CategoryID { get; set; }
            public string? ProductName { get; set; }
            public string? Description { get; set; }
            public double? UnitPrice { get; set; }
            public int? StockQuantity { get; set; }
            public string? Status { get; set; }
            public string? Image { get; set; }
        }

        public class UpdateProductModel
        {
            public int? CompanyID { get; set; }
            public int? CategoryID { get; set; }
            public string? ProductName { get; set; }
            public string? Description { get; set; }
            public double? UnitPrice { get; set; }
            public int? StockQuantity { get; set; }
            public string? Status { get; set; }
            public string? Image { get; set; }
        }

        public class ApiResponse<T>
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public T Data { get; set; }

            public ApiResponse(bool success, string message, T data)
            {
                Success = success;
                Message = message;
                Data = data;
            }
        }
    }
} 