using Microsoft.AspNetCore.SignalR;
using SocketTicTacToe.API.DTOs;
using TicTacToe.Core.Entities;
using TicTacToe.Core.Storages;

namespace SocketTicTacToe.API.Hubs
{
    public class GameHub : Hub
    {
        private readonly UsersStorage usersStorage;

        public GameHub(UsersStorage usersStorage)
        {
            this.usersStorage = usersStorage;
        }

        public override async Task OnConnectedAsync()
        {
            var userShape = usersStorage.GetAllowedShape();

            var output = new OnConnectDto();
            output.Shape = userShape.ToString();

            usersStorage.AddConnection(Context.ConnectionId,userShape);
            
            await Clients.Client(Context.ConnectionId).SendAsync("InitializePlayer", output);
        }
    }
}
