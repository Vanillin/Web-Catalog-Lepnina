using System.Net;

namespace Application.Exception
{
    public class EntityCreateException : ApplicationException
    {
        public EntityCreateException(string message) : base(message) { }
        public EntityCreateException(string message, System.Exception inner) : base(message, inner) { }

        public virtual HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public virtual string Title => "Entity is not create";
    }
}
