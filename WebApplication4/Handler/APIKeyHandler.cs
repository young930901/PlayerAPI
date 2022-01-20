using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Handler
{
    public class APIKeyHandler
    {
        private readonly RequestDelegate _next;
        private const string SECRET = "Secret";
        private const string CLIENTID = "ClientID";

        public APIKeyHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            //Check if header contains Secret Key
            if (!context.Request.Headers.TryGetValue(SECRET, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Secret Key was not provided.");
                return;
            }

            //Check if header contains Client ID
            if (!context.Request.Headers.TryGetValue(CLIENTID, out var extractedClientId))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Client ID was not provided.");
                return;
            }

            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();

            //Validate Secret
            var apiKey = appSettings.GetValue<string>(SECRET);

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Secret Key.");
                return;
            }

            //Validate Client Id
            var clientId = appSettings.GetValue<string>(CLIENTID);

            if (!clientId.Equals(extractedClientId))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Client Id.");
                return;
            }


            await _next(context);
        }
    }
}
