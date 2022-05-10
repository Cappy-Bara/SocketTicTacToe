using TicTacToe.Core.Entities;
using TicTacToe.Core.Exceptions;

namespace TicTacToe.Core.Storages
{
    public class UsersStorage
    {
        private readonly Dictionary<string, Shape> connections = new();

        public Shape GetAllowedShape()
        {
            int size = connections.Values.Count();

            if(size == 0)
                return Shape.O;

            if (size == 1)
                return connections.FirstOrDefault().Value == Shape.X ? Shape.O : Shape.X;

            return Shape.Empty;
        }
        public void AddConnection(string connectionId, Shape shape)
        {
            if (shape == Shape.Empty)
                throw new InternalStateException("Invalid user shape");

            connections.Add(connectionId, shape);
        }
        public void RemoveConnection(string connectionId)
        {
            connections.Remove(connectionId);
        }
        public int GetNumberOfPlayers()
        {
            return connections.Count;
        }
    }
}
