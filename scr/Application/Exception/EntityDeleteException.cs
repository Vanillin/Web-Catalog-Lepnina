using System.Net;

namespace Application.Exception
{
    public class EntityDeleteException : BaseApplicationException
    {
        public EntityDeleteException(string message) : base(message) { }
        public EntityDeleteException(string message, System.Exception inner) : base(message, inner) { }
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public override string Title => "Deleted transaction is not correct";
    }
}
