using System;
using System.Collections.Generic;

namespace Best.Core.Domain.DbEntities
{
    public class News : BaseEntity
    {
        public News()
        {
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
            Galeries = new HashSet<Galery>();
        }

        public string Title { get; set; }
        public string SeoTitle { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string TagNames { get; set; }
        public DateTime? PublishDate { get; set; }
        public int PublishUserId { get; set; }
        public int? AuthorId { get; set; }
        public int ReadCount { get; set; }
        public int CommentCount { get; set; }
        public string Source { get; set; }
        public bool IsPublished { get; set; }
        public string ProfileImgUrl { get; set; }
        public string ProfileImgUrlBig { get; set; }
        public string ProfileImgUrlMiddle { get; set; }
        public string ProfileImgUrlSmall { get; set; }
        public int CategoryId { get; set; }
        public int NewsTypeId { get; set; }
        public int NewsPositionId { get; set; }
        public int UserId { get; set; }

        public virtual Category Category { get; set; }
        public virtual NewsType NewsType { get; set; }
        public virtual NewsPosition NewsPosition { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Galery> Galeries{ get; set; }
    }
}
