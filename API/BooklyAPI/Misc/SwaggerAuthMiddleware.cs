using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace BooklyAPI.Misc;

public class SwaggerAuthMiddleware
{
    private readonly RequestDelegate next;

    public SwaggerAuthMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var header = AuthenticationHeaderValue.Parse(authHeader);
                var bytes = Convert.FromBase64String(header.Parameter!);
                var credentials = Encoding.UTF8.GetString(bytes).Split(':');
                var (username, password) = (credentials[0], credentials[1]);

                if (username.Equals("API") && password.Equals("API123"))
                {
                    await next.Invoke(context).ConfigureAwait(false);
                    return;
                }
            }
            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        else
        {
            await next.Invoke(context).ConfigureAwait(false);
        }
    }
}
