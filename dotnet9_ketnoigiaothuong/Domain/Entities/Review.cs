using System.ComponentModel.DataAnnotations;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        public int? SenderCompanyID { get; set; }
        public int? ReceiverCompanyID { get; set; }
        public int? ContractID { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? ReviewDate { get; set; }

        public virtual Company? SenderCompany { get; set; }
        public virtual Company? ReceiverCompany { get; set; }
        public virtual Contract? Contract { get; set; }
    }
}
