using Best.Core.Domain;
using Best.Data.UnitOfWork;
using Best.Web.Infrastructure.Enums;
using Best.Web.Infrastructure.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Best.Web.Infrastructure.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _uow;

        public BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    if (filterContext.ExceptionHandled)
        //        return;

        //    //Let the request know what went wrong
        //    Error(filterContext.Exception.Message);

        //    //redirect to error handler
        //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
        //        new { controller = "Common", action = "HandleError", area = "" }));

        //    // Stop any other exception handlers from running
        //    filterContext.ExceptionHandled = true;

        //    // CLear out anything already in the response
        //    filterContext.HttpContext.Response.Clear();

        //    base.OnException(filterContext);
        //}

        #region messages for view
        public void Default(string message)
        {
            AddMessage(message, MessageTypes.Default);
        }

        public void Info(string message)
        {
            AddMessage(message, MessageTypes.Info);
        }

        public void Success(string message)
        {
            AddMessage(message, MessageTypes.Success);
        }

        public void Warning(string message)
        {
            AddMessage(message, MessageTypes.Warning);
        }

        public void Error(string message)
        {
            AddMessage(message, MessageTypes.Danger);
        }
        #endregion

        #region private methods
        private void AddMessage(string message, MessageTypes type)
        {
            var messages = new List<MessageForView>();

            if (TempData[AppConstants.TempData_Key_MessageForView] != null)
                messages = (List<MessageForView>)TempData[AppConstants.TempData_Key_MessageForView];

            messages.Add(new MessageForView { MessageType = type, Message = message, MessageCode = -1 });

            TempData[AppConstants.TempData_Key_MessageForView] = messages;
        }
        #endregion
    }
}
