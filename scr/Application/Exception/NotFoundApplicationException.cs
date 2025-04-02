using System.Net;

namespace Application.Exception
{
    public class NotFoundApplicationException : BaseApplicationException
    {
        public NotFoundApplicationException(string message) : base(message) { }
        public NotFoundApplicationException(string message, System.Exception inner) : base(message, inner) { }
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public override string Title => "Entity not found";
    }
}
