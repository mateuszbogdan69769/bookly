namespace BooklyAPI.Misc;

public static class SwaggerExtensions
{
    public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SwaggerAuthMiddleware>();
    }
}
