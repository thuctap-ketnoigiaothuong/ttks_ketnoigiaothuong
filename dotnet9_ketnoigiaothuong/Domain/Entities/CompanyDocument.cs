using System.ComponentModel.DataAnnotations;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public class CompanyDocument
    {
        [Key]
        public int DocumentID { get; set; }
        public int? CompanyID { get; set; }
        public string? DocumentType { get; set; }
        public string? FilePath { get; set; }
        public DateTime? UploadDate { get; set; }

        public virtual Company? Company { get; set; }
    }
}
