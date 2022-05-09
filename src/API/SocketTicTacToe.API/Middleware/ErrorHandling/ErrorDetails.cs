using System.Net;

namespace SocketTicTacToe.API.Middleware.ErrorHandling
{
    public class ErrorDetails
    {
        public string ExceptionMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
