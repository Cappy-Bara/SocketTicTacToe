using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core.Entities
{
    public enum Shape
    {
        Empty,
        O,
        X
    }

    public static class ShapeExtensions
    {
        public static Shape SwapPlayer(this Shape shape)
        {
            shape = shape == Shape.O ? Shape.X : Shape.O;
            return shape;
        }
    }
}
