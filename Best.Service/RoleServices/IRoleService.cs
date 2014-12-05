using Best.Core.Domain.DbEntities;
using System.Linq;

namespace Best.Service.RoleServices
{
    public interface IRoleService
    {
        /// <summary>
        /// Rol role sahip mi.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        bool IsUserInRole(string userName, string roleName);

        /// <summary>
        /// Kullanıcıya göre roller.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IQueryable<Role> GetRolesByUserName(string userName);
    }
}
