using System.ComponentModel.DataAnnotations;

namespace dotnet9_ketnoigiaothuong.Domain.Entities
{
    public partial class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryImage { get; set; }
        public int? ParentCategoryID { get; set; }

        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
