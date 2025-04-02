using System.Net;

namespace Application.Exception
{
    public class AlreadyExistsApplicationException : BaseApplicationException
    {
        public AlreadyExistsApplicationException(string message) : base(message) { }
        public AlreadyExistsApplicationException(string message, System.Exception inner) : base(message, inner) { }
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public override string Title => "Entity already exists";
    }
}
