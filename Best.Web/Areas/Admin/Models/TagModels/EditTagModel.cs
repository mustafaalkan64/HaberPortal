using Best.Web.Infrastructure.Model;
using System.ComponentModel.DataAnnotations;

namespace Best.Web.Areas.Admin.Models.TagModels
{
    public class EditTagModel:BaseViewModel
    {
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Etiket Adı")]
        public string Name { get; set; }
    }
}