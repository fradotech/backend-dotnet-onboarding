public class AuthorizationMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("AuthorizationMiddleware: Testing middleware");
        await _next(context);
    }
}