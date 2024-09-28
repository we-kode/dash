using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Dash.Server.Middleware;

public class ApiKeyValidator(RequestDelegate next, IConfiguration configuration)
{
    private readonly RequestDelegate _next = next;
    private readonly IConfiguration _configuration = configuration;

    public async Task Invoke(HttpContext context)
    {
        // TODO: Check if user is logged in or display share key header is provided and saved in db
        var isValid = true;
        if (!isValid)
        {
            await UnauthorizedRespone(context);
            return;
        }

        await _next.Invoke(context);
    }

    private static async Task UnauthorizedRespone(HttpContext context)
    {
        context.Response.StatusCode = 401; //Unauthorized               
        await context.Response.WriteAsync(string.Empty);
    }
}

public static class ApiKeyValidatorExtension
{
    public static IApplicationBuilder UseApiKeyValidation(this IApplicationBuilder app)
    {
        app.UseMiddleware<ApiKeyValidator>();
        return app;
    }
}
