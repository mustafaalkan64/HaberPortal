using Best.Core.Domain;
using Best.Core.Domain.DbEntities;
using Best.Data.UnitOfWork;
using Best.Service.UserServices;
using Best.Web.Infrastructure.Controllers;
using Best.Web.Models.AccountModels;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;

namespace Best.Web.Controllers
{
    public class AccountController : PublicController
    {
        private readonly IUserService _userService;

        public AccountController(IUnitOfWork uow, IUserService userService)
            : base(uow)
        {
            _userService = userService;
        }

        public ActionResult _Login()
        {
            return PartialView();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = new User
                    {
                        ConfirmationId = Guid.NewGuid(),
                        IsConfirmed = true,
                        LastLoginDate = DateTime.Now,
                        LastLoginIP = Request.UserHostAddress,
                        Password = model.Password,
                        ProfileImageUrl = "Content/Images/Users/no_profile_image.png",
                        Email = model.Email,
                        UserName = model.UserName,
                        IsActive = true,
                        InsertDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        InsertUserId = 0,
                        UpdateUserId = 0
                    };

                    _userService.Insert(user);
                    _uow.SaveChanges();
                    //_userService.SendConfirmationMail(user, Request.Url.GetLeftPart(UriPartial.Authority));

                    FormsAuthentication.SetAuthCookie(model.UserName, true);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Kullanıcı oluşturma başarısız! Hata: " + ex.Message);
                }
            }

            return View(model);
        }

        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string ReturnUrl)
        {
            var user = _userService.FindByUserNameAndPassword(model.UserName, model.Password);
            if (ModelState.IsValid && user != null)
            {
                if (!user.IsConfirmed)
                {
                    TempData[AppConstants.TempData_Key_EmailConfirm] = "E-posta adresiniz onaylı değildir. Lütfen e-posta adresinizdeki linki kullanarak e-posta adresinizi onaylayınız.";

                    return View();
                }
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                return RedirectToLocal(ReturnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı ve ya şifre geçersiz!");
            }

            return View(model);
        }

        // RegisterModel içerisindeki Email alanını
        // RemoteAttribute ile kontrol eder
        public JsonResult ValidateEmail(string Email)
        {
            var result = _userService.ValidateEmail(Email);

            if (result)
            {
                return Json("Girdiğiniz e-posta adresi sistemde zaten mevcut!", JsonRequestBehavior.AllowGet);
            }

            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        // RegisterModel içerisindeki UserName alanını
        // RemoteAttribute ile kontrol eder
        public JsonResult ValidateUserName(string UserName)
        {
            var result = _userService.ValidateUserName(UserName);

            if (result)
            {
                return Json("Girdiğiniz kullanıcı adı sistemde zaten mevcut!", JsonRequestBehavior.AllowGet);
            }

            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ConfirmUser(Guid confirmationId)
        {
            if (string.IsNullOrEmpty(confirmationId.ToString()) || (!Regex.IsMatch(confirmationId.ToString(),
                   @"[0-9a-f]{8}\-([0-9a-f]{4}\-){3}[0-9a-f]{12}")))
            {
                TempData[AppConstants.TempData_Key_EmailConfirm] = "Hesap geçerli değil. Lütfen e-posta adresinizdeki linke tekrar tıklayınız.";

                return View();
            }
            else
            {
                var user = _userService.FindByConfirmationId(confirmationId);

                if (!user.IsConfirmed)
                {
                    user.IsConfirmed = true;
                    _userService.Update(user);
                    _uow.SaveChanges();

                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    TempData[AppConstants.TempData_Key_EmailConfirm] = "E-posta adresinizi onayladığınız için teşekkürler. Artık sitemize üyesiniz.";

                    return RedirectToAction("Wellcome");
                }
                else
                {
                    TempData[AppConstants.TempData_Key_EmailConfirm] = "E-posta adresiniz zaten onaylı. Giriş yapabilirsiniz.";

                    return RedirectToAction("GirisYap");
                }
            }
        }

        public ActionResult Wellcome()
        {
            return View();
        }

        #region private methods
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}