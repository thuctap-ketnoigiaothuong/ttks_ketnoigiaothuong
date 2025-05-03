using System.ComponentModel.DataAnnotations;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }
        public int? CompanyID { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }

        public virtual Company? Company { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
