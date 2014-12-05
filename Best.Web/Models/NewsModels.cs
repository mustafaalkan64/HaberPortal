using Best.Core.Domain.DbEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Best.Web.Models
{
    public class CommentModel
    {
        public int NewsId { get; set; }

        public IQueryable<Comment> Comments { get; set; }
    }

    public class EditCommentModel {
        public int NewsId { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Yorum Ekle")]
        public string CommentBody { get; set; }
    }
}