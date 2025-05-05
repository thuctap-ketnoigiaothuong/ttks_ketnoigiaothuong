using System.ComponentModel.DataAnnotations;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }
        public int? UserID { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsRead { get; set; }

        public virtual UserAccount? User { get; set; }
    }
}
