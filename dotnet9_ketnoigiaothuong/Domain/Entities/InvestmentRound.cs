using System.ComponentModel.DataAnnotations;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public class InvestmentRound
    {
        [Key]
        public int RoundID { get; set; }
        public int? ContractID { get; set; }
        public string? ProjectName { get; set; }
        public string? StageName { get; set; }
        public double? InvestmentRate { get; set; }
        public double? InvestmentAmount { get; set; }
        public double? ProfitCommitment { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public double? Progress { get; set; }
        public string? Status { get; set; }
        public double? ActualRevenue { get; set; }
        public double? ActualProfit { get; set; }
        public string? Notes { get; set; }

        public virtual Contract? Contract { get; set; }
    }
}
