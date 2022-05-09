using System.Net;

namespace SocketTicTacToe.API.Middleware.ErrorHandling
{
    public static class ErrorDetailsExtension
    {
        internal static ErrorDetails ToErrorDetails(this Exception ex, HttpStatusCode code = HttpStatusCode.InternalServerError)
        {
            return new ErrorDetails()
            {
                StatusCode = code,
                ExceptionMessage = ex.Message
            };
        }
    }
}
