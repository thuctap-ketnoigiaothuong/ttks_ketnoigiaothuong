using System.ComponentModel.DataAnnotations;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public class QuotationResponse
    {
        [Key]
        public int ResponseID { get; set; }
        public int? RequestID { get; set; }
        public int? BuyerCompanyID { get; set; }
        public int? SellerCompanyID { get; set; }
        public double? ProposedPrice { get; set; }
        public string? DeliveryTime { get; set; }
        public string? ShippingMethod { get; set; }
        public string? Terms { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual QuotationRequest? QuotationRequest { get; set; }
        public virtual Company? BuyerCompany { get; set; }
        public virtual Company? SellerCompany { get; set; }
    }
}
