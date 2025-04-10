using System.Net;

namespace Application.Exception
{
    public class BaseApplicationException : ApplicationException
    {
        public BaseApplicationException(string message) : base(message) { }
        public BaseApplicationException(string message, System.Exception inner) : base(message, inner) { }

        public virtual HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public virtual string Title => "Application Exception occured";
    }
}
