using Best.Core.Domain.DbEntities;
using Best.Service.CategoryServices;
using Best.Web.Infrastructure.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Best.Web.Areas.Admin.Models.CategoryModels
{
    public class EditCategoryModel : BaseViewModel
    {
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Kategori Resim")]
        public HttpPostedFileBase ProfileImg { get; set; }

        public string ProfileImgUrl { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Sıra")]
        public int Order { get; set; }

        [Display(Name = "Üst Kategori")]
        public int? ParentId { get; set; }

        public virtual IEnumerable<Category> Categories { get; set; }
    }
}