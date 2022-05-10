using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTicTacToe.API.DTOs
{
    public class FigurePlacedDto
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public string Shape { get; set; }
    }
}
