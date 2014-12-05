using Best.Core.Domain.DbEntities;
using Best.Web.Infrastructure.Model;
using Best.Web.Infrastructure.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Best.Web.Areas.Admin.Models.NewsModels
{
    public class EditNewsModel : BaseViewModel
    {
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Kısa Açıklama")]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "İçerik")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Kaynak")]
        public string Source { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Yayında")]
        public bool IsPublished { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Profil Resim")]
        public HttpPostedFileBase ProfileImg { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Haber Tipi")]
        public int NewsTypeId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Haber Pozisyon")]
        public int NewsPositionId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Etiketler")]
        public int[] SelectedTagIds { get; set; }

        [Display(Name = "Galeriler")]
        public int[] SelectedGaleryIds { get; set; }

        [Display(Name = "Yazar")]
        public int? AuthorId { get; set; }

        public string ProfileImgUrl { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<NewsType> NewsTypes { get; set; }
        public IEnumerable<NewsPosition> NewsPositions { get; set; }
        public IEnumerable<User> Authors { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Galery> Galeries { get; set; }
    }
}