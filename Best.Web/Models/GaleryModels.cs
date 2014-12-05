using Best.Core.Domain.DbEntities;
using System.Linq;

namespace Best.Web.Models
{
    public class GaleryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeoName { get; set; }

        public virtual IQueryable<GaleryImage> GaleryImages { get; set; }
    }
}