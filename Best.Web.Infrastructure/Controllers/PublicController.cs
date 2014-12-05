using Best.Data.UnitOfWork;
using Best.Web.Infrastructure.Extensions;

namespace Best.Web.Infrastructure.Controllers
{
    public class PublicController : BaseController
    {
        public PublicController(IUnitOfWork uow)
            : base(uow)
        {
        }
    }
}
