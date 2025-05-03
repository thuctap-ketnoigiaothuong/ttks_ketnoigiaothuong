using System.ComponentModel.DataAnnotations;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public class Contract
    {
        [Key]
        public int ContractID { get; set; }
        public int? SellerCompanyID { get; set; }
        public int? BuyerCompanyID { get; set; }
        public string? ContractType { get; set; }
        public string? Title { get; set; }
        public string? Terms { get; set; }
        public string? DisputeResolution { get; set; }
        public string? Status { get; set; }
        public string? SellerSignature { get; set; }
        public string? BuyerSignature { get; set; }
        public DateTime? SignDate { get; set; }
        public string? ContractFile { get; set; }
        public bool? DigitalSignature { get; set; }

        public virtual Company? SellerCompany { get; set; }
        public virtual Company? BuyerCompany { get; set; }
        public virtual ICollection<PeriodicTransaction> PeriodicTransactions { get; set; }
        public virtual ICollection<InvestmentRound> InvestmentRounds { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}
