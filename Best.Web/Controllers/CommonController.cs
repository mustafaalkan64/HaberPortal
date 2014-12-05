using Best.Data.UnitOfWork;
using Best.Web.Infrastructure.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Best.Web.Controllers
{
    public class CommonController : PublicController
    {
        public CommonController(IUnitOfWork uow)
            : base(uow)
        {

        }

        public ActionResult HandleError()
        {
            return View();
        }
        public ActionResult Robots()
        {
            Response.ContentType = "text/plain";
            return View();
        }
    }
}