using System.Collections.Generic;

namespace Best.Core.Domain.DbEntities
{
    public class Tag : BaseEntity
    {
        public Tag()
        {
            News = new HashSet<News>();
        }

        public string Name { get; set; }
        public string SeoName { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
