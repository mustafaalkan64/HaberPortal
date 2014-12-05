using Best.Core.Domain.DbEntities;
using Best.Data.Repositories;
using Best.Data.UnitOfWork;
using System.Linq;

namespace Best.Service.RoleServices
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<User> _userRepository;

        public RoleService(IUnitOfWork uow)
        {
            _userRepository = uow.GetRepository<User>();
        }

        /// <summary>
        /// Kullanıcıya göre roller.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IQueryable<Role> GetRolesByUserName(string userName)
        {
            return _userRepository.GetAll().FirstOrDefault(x => x.UserName == userName).Roles.AsQueryable();
        }

        /// <summary>
        /// Rol role sahip mi.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool IsUserInRole(string userName, string roleName)
        {
            return _userRepository.GetAll().FirstOrDefault(x => x.UserName == userName).Roles.Any(x => x.Name == roleName);
        }
    }
}
