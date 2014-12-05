using System.Collections.Generic;

namespace Best.Core.Domain.DbEntities
{
    public class NewsPosition : BaseEntity
    {
        public NewsPosition()
        {
            News = new HashSet<News>();
        }

        public string Name { get; set; }
        public int PositionId { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
