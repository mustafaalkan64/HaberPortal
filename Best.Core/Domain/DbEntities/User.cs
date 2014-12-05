using System;
using System.Collections.Generic;

namespace Best.Core.Domain.DbEntities
{
    public class User : BaseEntity
    {
        public User()
        {
            Roles = new HashSet<Role>();
            Comments = new HashSet<Comment>();
        }

        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string LastLoginIP { get; set; }
        public int Point { get; set; }
        public string ProfileImageUrl { get; set; }
        public Guid ConfirmationId { get; set; }
        public bool IsConfirmed { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
