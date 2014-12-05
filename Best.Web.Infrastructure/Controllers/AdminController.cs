using Best.Core.Domain;
using Best.Data.UnitOfWork;
using System.Web.Mvc;

namespace Best.Web.Infrastructure.Controllers
{
    [Authorize(Roles = AppConstants.Role_Admin)]
    public class AdminController : BaseController
    {
        public AdminController(IUnitOfWork uow)
            : base(uow)
        {
        }
    }
}
