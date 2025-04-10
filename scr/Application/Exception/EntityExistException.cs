using System.Net;

namespace Application.Exception
{
    public class EntityExistException : BaseApplicationException
    {
        public EntityExistException(string message) : base(message) { }
        public EntityExistException(string message, System.Exception inner) : base(message, inner) { }
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public override string Title => "Entity already exists";
    }
}
