using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public class QuotationRequest
    {
        [Key]
        public int RequestID { get; set; }
        public int? BuyerCompanyID { get; set; }
        public int? SellerCompanyID { get; set; }
        public int? ProductID { get; set; }
        public int? Quantity { get; set; }
        public string? DeliveryTime { get; set; }
        public string? AdditionalRequest { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Company? BuyerCompany { get; set; }
        public virtual Company? SellerCompany { get; set; }
        public virtual Product? Product { get; set; }
        public virtual ICollection<QuotationResponse> QuotationResponses { get; set; }
    }
}
