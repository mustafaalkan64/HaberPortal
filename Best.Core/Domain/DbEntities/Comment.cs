using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Best.Core.Domain.DbEntities
{
    public class Comment : BaseEntity
    {
        public string _Content { get; set; }

        public int UserId { get; set; }
        public int NewsId { get; set; }
        public virtual User User { get; set; }
        public virtual News News { get; set; }
    }
}
