using System.Net;

namespace Application.Exception
{
    public class EntityUpdateException : ApplicationException
    {
        public EntityUpdateException(string message) : base(message) { }
        public EntityUpdateException(string message, System.Exception inner) : base(message, inner) { }

        public virtual HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public virtual string Title => "Service not update entity";
    }
}
