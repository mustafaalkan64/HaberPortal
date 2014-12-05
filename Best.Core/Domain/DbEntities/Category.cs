using System.Collections.Generic;
namespace Best.Core.Domain.DbEntities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            News = new HashSet<News>();
        }

        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string SeoName { get; set; }
        public string NodePathIds { get; set; }
        public string NodePathText { get; set; }
        public string ProfileImgUrl { get; set; }
        public int Order { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
