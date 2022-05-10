using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTicTacToe.API.DTOs
{
    public class MakeMoveDto
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Shape { get; set; }
    }
}
