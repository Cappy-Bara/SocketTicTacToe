using Microsoft.AspNetCore.SignalR;
using SocketTicTacToe.API.DTOs;
using TicTacToe.Core.Entities;
using TicTacToe.Core.Storages;

namespace SocketTicTacToe.API.Hubs
{
    public class GameHub : Hub
    {
        private readonly UsersStorage usersStorage;
        private readonly Board board;

        public GameHub(UsersStorage usersStorage, Board board)
        {
            this.usersStorage = usersStorage;
            this.board = board;
        }

        public override async Task OnConnectedAsync()
        {
            var userShape = usersStorage.GetAllowedShape();

            var output = new OnConnectDto();
            output.Shape = userShape.ToString();

            usersStorage.AddConnection(Context.ConnectionId, userShape);

            await Clients.Client(Context.ConnectionId).SendAsync("InitializePlayer", output);

            await TryBeginGame();
        }

        public async Task MakeMove(MakeMoveDto message)
        {
            var playingFigure = (Shape)message.Shape;

            board.PlaceFigure(message.PosX, message.PosY, playingFigure);

            if (board.GameIsFinished)
            {
                var gameFinishedData = new GameFinishedDto()
                {
                    Winner = board.Winner.ToString(),
                };

                await Clients.All.SendAsync("GameFinished", gameFinishedData);
                return;
            }

            playingFigure.SwapPlayer();

            var nextTurnData = new NextTurnDto()
            {
                Shape = playingFigure.ToString(),
            };

            await Clients.All.SendAsync("NextTurn", nextTurnData);
        }

        public async Task Reset()
        {
            board.FlushBoard();
            await TryBeginGame();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var disconnectedUser = Context.ConnectionId;

            usersStorage.RemoveConnection(disconnectedUser);

            if(usersStorage.GetNumberOfPlayers() < 2)
                await Clients.AllExcept(disconnectedUser).SendAsync("UserDisconnected");
        }


        private async Task TryBeginGame()
        {
            if (usersStorage.GetNumberOfPlayers() == 2)
            {
                var data = new NextTurnDto()
                {
                    Shape = Shape.O.ToString(),
                };

                await Clients.All.SendAsync("NextTurn", data);
            }
        }
    }
}
