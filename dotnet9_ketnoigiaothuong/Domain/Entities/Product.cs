using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public int? CompanyID { get; set; }
        public int? CategoryID { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public double? UnitPrice { get; set; }
        public int? StockQuantity { get; set; }
        public string? Status { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public string? ApprovalNotes { get; set; }

        public virtual Company? Company { get; set; }
        public virtual Category? Category { get; set; }
        public virtual UserAccount? ApprovedByUser { get; set; }
        public virtual ICollection<QuotationRequest> QuotationRequests { get; set; }
    }
}
