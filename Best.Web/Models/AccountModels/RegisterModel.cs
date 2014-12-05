using System.ComponentModel.DataAnnotations;

namespace Best.Web.Models.AccountModels
{
    public class RegisterModel
    {
        [StringLength(150, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Kullanıcı Adı")]
        [System.Web.Mvc.Remote("ValidateUserName", "Account")]
        public string UserName { get; set; }



        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "İki şifre eşleşmiyor!")]
        [Display(Name = "Şifre Tekrar")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [StringLength(150, ErrorMessage = "{0} alanı en fazla {1} karakter uzunluğunda olmalıdır!")]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [EmailAddress(ErrorMessage = "{0} geçersiz!")]
        [Display(Name = "E-Posta")]
        [System.Web.Mvc.Remote("ValidateEmail", "Account")]
        public string Email { get; set; }
    }
}