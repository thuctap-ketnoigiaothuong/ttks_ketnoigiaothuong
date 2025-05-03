using System.ComponentModel.DataAnnotations;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        public int? ContractID { get; set; }
        public double? Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Method { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }

        public virtual Contract? Contract { get; set; }
    }
}
