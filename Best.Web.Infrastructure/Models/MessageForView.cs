using Best.Web.Infrastructure.Enums;

namespace Best.Web.Infrastructure.Models
{
    public class MessageForView
    {
        public string Message { get; set; }
        public MessageTypes MessageType { get; set; }
        public int MessageCode { get; set; }
    }
}
