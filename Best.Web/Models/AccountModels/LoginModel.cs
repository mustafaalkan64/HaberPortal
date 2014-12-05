using System.ComponentModel.DataAnnotations;

namespace Best.Web.Models.AccountModels
{
    public class LoginModel
    {
        [StringLength(150, ErrorMessage = "{0} alanı en fazla {1} karakter uzunluğunda olmalıdır!")]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla?")]
        public bool RememberMe { get; set; }
    }
}