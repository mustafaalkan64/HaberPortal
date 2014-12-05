using System.Collections.Generic;

namespace Best.Core.Domain.DbEntities
{
    public class Role : BaseEntity
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
