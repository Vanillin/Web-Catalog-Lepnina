using System.Net;

namespace Application.Exception
{
    public class TransactionApplicationException : BaseApplicationException
    {
        public TransactionApplicationException(string message) : base(message) { }
        public TransactionApplicationException(string message, System.Exception inner) : base(message, inner) { }
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public override string Title => "Deleted transaction is not correct";
    }
}
