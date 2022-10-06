namespace OddIsOdd;

public class OddIsOddMiddleware
{
    private readonly RequestDelegate _next;

    private readonly Func<bool> isEven = () => DateTime.UtcNow.Second % 2 == 0;

    public OddIsOddMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Path.HasValue) return;

        if (context.Request.Path.Value.Equals("/"))
        {
            context.Response.Redirect(isEven()
                ? Constants.PathToEven
                : Constants.PathToOdd);
            return; // short circuit!
        }

        if (context.Request.Path.Value.Equals(Constants.PathToEven) && !isEven())
        {
            context.Response.Redirect(Constants.PathToOdd);
            return;
        }

        if (context.Request.Path.Value.Equals(Constants.PathToOdd) && isEven())
        {
            context.Response.Redirect(Constants.PathToEven);
            return;
        }

        await _next(context);
    }
}

public static class OddIsOddMiddlewareExtension
{
    public static IApplicationBuilder UseOddIsOddMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<OddIsOddMiddleware>();
    }
}
