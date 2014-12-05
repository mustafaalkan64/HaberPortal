using Best.Core.Domain;
using Best.Core.Domain.DbEntities;
using Best.Data.Repositories;
using Best.Data.UnitOfWork;
using System;
using System.Linq;
using System.Net.Mail;

namespace Best.Service.UserServices
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IUnitOfWork uow)
        {
            _userRepository = uow.GetRepository<User>();
        }

        /// <summary>
        /// Kullanıcı bul.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User FindByUserNameAndPassword(string userName, string password)
        {
            return _userRepository.GetAll().FirstOrDefault(x => x.UserName == userName && x.Password == password);
        }

        /// <summary>
        /// Kullanıcı bul.
        /// </summary>
        /// <param name="confirmationId"></param>
        /// <returns></returns>
        public User FindByConfirmationId(Guid confirmationId)
        {
            return _userRepository.GetAll().FirstOrDefault(x => x.ConfirmationId == confirmationId);
        }

        /// <summary>
        /// Kullanıcı ekle.
        /// </summary>
        /// <param name="user"></param>
        public void Insert(User user)
        {
            _userRepository.Insert(user);
        }

        /// <summary>
        /// Kullanıcı güncelle.
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="userId">Kullanıcı Id</param>
        public User Delete(int userId)
        {
            var user = _userRepository.Find(userId);

            _userRepository.Delete(user);

            return user;
        }

        /// <summary>
        /// Yeni üye olan kullanıcıya onay mesajı gönder.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ConfirmationUrl"></param>
        /// <returns>Email send success status</returns>
        public bool SendConfirmationMail(User user, string ConfirmationUrl)
        {
            var status = false;
            string confirmationId = user.ConfirmationId.ToString();
            ConfirmationUrl += "/Account/ConfirmUser?confirmationId=" + confirmationId;

            var message = new MailMessage("info@haberportal.com", user.Email)
            {
                Subject = "Lütfen e-posta adresinizi onaylayınız.",
                Body = ConfirmationUrl
            };

            var client = new SmtpClient();
            try
            {
                client.Send(message);
                status = true;
            }
            catch (System.Exception)
            {
                return status;
            }

            return status;
        }

        /// <summary>
        /// Eposta sistemde kayıtlı mı.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ValidateEmail(string email)
        {
            return _userRepository.GetAll().Any(x => x.Email == email);
        }

        /// <summary>
        /// Kullanıcı adı sistemde kayıtlı mı.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool ValidateUserName(string userName)
        {
            return _userRepository.GetAll().Any(x => x.UserName == userName);
        }

        /// <summary>
        /// Tüm yazarlar.
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetAllAuthors()
        {
            return _userRepository.GetAll()
                .Where(x => x.Roles.Any(y => y.Name == AppConstants.Role_Author));
        }

        /// <summary>
        /// Kullancıcı bul.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User Find(int userId)
        {
            return _userRepository.Find(userId);
        }
    }
}
