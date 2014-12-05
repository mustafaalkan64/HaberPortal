using System.Collections.Generic;

namespace Best.Core.Domain.DbEntities
{
    public class Galery : BaseEntity
    {
        public Galery()
        {
            GaleryImages = new HashSet<GaleryImage>();
        }

        public string Name { get; set; }
        public int? NewsId { get; set; }
        public string SeoName { get; set; }

        public virtual News News { get; set; }

        public virtual ICollection<GaleryImage> GaleryImages { get; set; }
    }
}
