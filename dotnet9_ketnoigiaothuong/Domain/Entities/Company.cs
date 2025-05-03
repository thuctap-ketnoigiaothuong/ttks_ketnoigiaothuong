using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }
        public string? CompanyName { get; set; }
        public string? TaxCode { get; set; }
        public string? BusinessSector { get; set; }
        public string? Address { get; set; }
        public string? Representative { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string? VerificationStatus { get; set; }
        public string? LegalDocuments { get; set; }

        public virtual ICollection<CompanyDocument> CompanyDocuments { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<QuotationRequest> QuotationRequestsAsBuyer { get; set; }
        public virtual ICollection<QuotationRequest> QuotationRequestsAsSeller { get; set; }
        public virtual ICollection<QuotationResponse> QuotationResponsesAsBuyer { get; set; }
        public virtual ICollection<QuotationResponse> QuotationResponsesAsSeller { get; set; }
        public virtual ICollection<Contract> SellerContracts { get; set; }
        public virtual ICollection<Contract> BuyerContracts { get; set; }
        public virtual ICollection<Review> ReviewsAsSender { get; set; }
        public virtual ICollection<Review> ReviewsAsReceiver { get; set; }
    }
}
