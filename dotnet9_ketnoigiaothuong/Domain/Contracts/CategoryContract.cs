using System.Collections.Generic;

namespace dotnet9_ketnoigiaothuong.Domain.Contracts
{
    public class CategoryContract
    {
        public class CategoryListItem
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public int? ParentCategoryID { get; set; }
            public string ParentCategoryName { get; set; }
        }

        public class CategoryDetailModel
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public int? ParentCategoryID { get; set; }
            public string ParentCategoryName { get; set; }
            public List<CategoryListItem> SubCategories { get; set; } = new List<CategoryListItem>();
        }

        public class CreateCategoryModel
        {
            public string CategoryName { get; set; }
            public int? ParentCategoryID { get; set; }
        }

        public class UpdateCategoryModel
        {
            public string CategoryName { get; set; }
            public int? ParentCategoryID { get; set; }
        }
    }
} 