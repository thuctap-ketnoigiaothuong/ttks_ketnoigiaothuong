using System.ComponentModel.DataAnnotations;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public class PeriodicTransaction
    {
        [Key]
        public int TransactionID { get; set; }
        public int? ContractID { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Status { get; set; }
        public string? InvoiceFile { get; set; }
        public string? ReportFile { get; set; }

        public virtual Contract? Contract { get; set; }
    }
}
