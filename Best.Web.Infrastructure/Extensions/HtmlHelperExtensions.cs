using Best.Core.Domain;
using Best.Web.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc.Html
{
    public static class DisplayExtensions
    {
        public static MvcHtmlString DisplayMessage(this HtmlHelper htmlHelper)
        {
            var tempMessages = htmlHelper.ViewContext.TempData[AppConstants.TempData_Key_MessageForView];
            if (tempMessages == null) return MvcHtmlString.Create(string.Empty);

            var messages = (List<MessageForView>)tempMessages;

            var messageToDisplay = "<div class=\"msg\">";

            messageToDisplay = messages.Aggregate(messageToDisplay, (s, msg) =>
            {
                s += "<div class=\"alert alert-" + msg.MessageType.ToString().ToLower() + " alert-dismissable\">" +
                    "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>" +
                    msg.Message +
                    "</div>";
                return s;
            });

            messageToDisplay += "</div>";

            return MvcHtmlString.Create(messageToDisplay);
        }
    }
}
