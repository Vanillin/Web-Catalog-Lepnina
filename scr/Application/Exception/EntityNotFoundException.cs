using System.Net;

namespace Application.Exception
{
    public class EntityNotFoundException : BaseApplicationException
    {
        public EntityNotFoundException(string message) : base(message) { }
        public EntityNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public override string Title => "Entity not found";
    }
}
