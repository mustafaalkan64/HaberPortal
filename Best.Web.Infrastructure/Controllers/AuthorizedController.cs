using Best.Data.UnitOfWork;
using System.Web.Mvc;

namespace Best.Web.Infrastructure.Controllers
{
    [Authorize]
    public class AuthorizedController : BaseController
    {
        public AuthorizedController(IUnitOfWork uow)
            : base(uow)
        {
        }
    }
}
