using System.Net;

namespace Application.Exception
{
    public class MappingApplicationException : BaseApplicationException
    {
        public MappingApplicationException(string message) : base(message) { }
        public MappingApplicationException(string message, System.Exception inner) : base(message, inner) { }
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public override string Title => "Mapping is not correct";
    }
}
