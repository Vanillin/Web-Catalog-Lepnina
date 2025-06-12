using System.Net;

namespace Application.Exception
{
    class ArgumentIsNullException : ApplicationException
    {
        public ArgumentIsNullException(string message) : base(message) { }
        public ArgumentIsNullException(string message, System.Exception inner) : base(message, inner) { }

        public virtual HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public virtual string Title => "Argument is null";
    }
}
