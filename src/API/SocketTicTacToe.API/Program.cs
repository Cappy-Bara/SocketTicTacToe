using SocketTicTacToe.API.Hubs;
using SocketTicTacToe.API.Middleware.ErrorHandling;
using TicTacToe.Core.Entities;
using TicTacToe.Core.Storages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Board>();
builder.Services.AddSingleton<UsersStorage>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.MapHub<GameHub>("/play");

app.UseAuthorization();

app.MapControllers();

app.Run();
