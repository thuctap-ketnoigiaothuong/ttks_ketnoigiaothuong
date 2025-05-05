using System.ComponentModel.DataAnnotations;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public class TransactionHistory
    {
        [Key]
        public int HistoryID { get; set; }
        public int? ContractID { get; set; }
        public string? Action { get; set; }
        public int? PerformedByUserID { get; set; }
        public DateTime? ActionTime { get; set; }
        public string? Notes { get; set; }

        public virtual Contract? Contract { get; set; }
        public virtual UserAccount? PerformedByUser { get; set; }
    }
}
