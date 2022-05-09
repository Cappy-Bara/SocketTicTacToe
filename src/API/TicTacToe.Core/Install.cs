using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TicTacToe.Core
{
    public static class Install
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseCore(this IApplicationBuilder app)
        {
            return app;
        }
    }
}