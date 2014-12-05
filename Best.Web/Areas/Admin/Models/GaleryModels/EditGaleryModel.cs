using Best.Web.Infrastructure.Model;
using System.ComponentModel.DataAnnotations;

namespace Best.Web.Areas.Admin.Models.GaleryModels
{
    public class EditGaleryModel : BaseViewModel
    {
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Galeri Adı")]
        public string Name { get; set; }
    }

    public class EditGaleryImageModel : BaseViewModel
    {
        public string GaleryName { get; set; }
        public int GaleryId { get; set; }
        public string ImgUrl { get; set; }
    }
}