using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core.Exceptions;

namespace TicTacToe.Core.Entities
{
    public class Board
    {
        private readonly Dictionary<(int, int), Shape> fields = new();

        public bool IsFinished { get; private set; } = false;
        public Shape Winner { get; private set; } = Shape.Empty;
        public Board()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    fields[(i, j)] = Shape.Empty;
        }
        

        public void PlaceFigure(int posX, int posY, Shape placedValue)
        {
            if (placedValue == Shape.Empty)
                throw new GameRuleException("Invalid shape.");

            if (fields[(posX, posY)] != Shape.Empty)
                throw new GameRuleException("Field already occupied.");

            fields[(posX, posY)] = placedValue;

            CheckWinAndSetWinner(placedValue);
        }
        public void FlushBoard()
        {
            fields.Clear();
        }


        private void CheckWinAndSetWinner(Shape lastPlayer)
        {
            IsFinished = CheckIfBoardFull();

            bool somebodyWon = CheckRowsAndCollumns() || CheckCross();

            if (somebodyWon)
            {
                Winner = lastPlayer;
                IsFinished = true;
            }
        }
        private bool CheckIfBoardFull()
        {
            return fields.Values.Any(x => x == Shape.Empty);
        }
        private bool CheckRowsAndCollumns()
        {
            for (int i = 0; i < 3; i++)
            {
                var finishedByRow = CheckRow(i);
                var finishedByCollumn = CheckCollumn(i);

                if (finishedByCollumn || finishedByRow)
                {
                    return true;
                } 
            }
            return false;
        }
        private bool CheckRow(int i)
        {
            var values = fields.Where(x => new List<(int, int)>() { (i, 0), (i, 1), (i, 2) }.Contains(x.Key))
                                   .Select(x => x.Value)
                                   .Distinct();

            return CheckWin(values);

        }
        private bool CheckCollumn(int i)
        {
            var values = fields.Where(x => new List<(int, int)>() { (0, i), (1, i), (2, i) }.Contains(x.Key))
           .Select(x => x.Value)
           .Distinct();

            return CheckWin(values);
        }
        private bool CheckCross()
        {
            var valuesFirst = fields.Where(x => new List<(int, int)>() { (0, 0), (1, 1), (2, 2) }.Contains(x.Key))
                .Select(x => x.Value)
                .Distinct();

            if (CheckWin(valuesFirst))
                return true;

            var valuesSecond = fields.Where(x => new List<(int, int)>() { (0, 0), (1, 1), (2, 2) }.Contains(x.Key))
                .Select(x => x.Value)
                .Distinct();

            return CheckWin(valuesSecond);
        }
        private static bool CheckWin(IEnumerable<Shape> values)
        {
            return values.Count() == 1 && values.All(x => x != Shape.Empty);
        }
    }
}
