using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core.Exceptions
{
    public abstract class SocketTicTacToeException : Exception
    {
        public int StatusCode { get; set; }
        public SocketTicTacToeException(string message, int code) : base(message)
        {
            StatusCode = code;
        }
    }
}
