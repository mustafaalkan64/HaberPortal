using Best.Data.UnitOfWork;
using Best.Web.Infrastructure.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Best.Web.Areas.Admin.Controllers
{
    public class DashboardController : AdminController
    {
        public DashboardController(IUnitOfWork uow)
            : base(uow)
        {

        }

        public ActionResult Index()
        {
            return View();
        }
    }
}