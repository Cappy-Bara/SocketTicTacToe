using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core.Exceptions
{
    public class GameRuleException : SocketTicTacToeException
    {
        public GameRuleException(string message) : base(message, 400)
        {
        }
    }
}
