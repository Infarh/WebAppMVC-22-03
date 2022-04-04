namespace WebAppMVC.Infrastructure.Middleware;

public class TestMiddleware
{
    private readonly RequestDelegate _Next;

    public TestMiddleware(RequestDelegate Next) => _Next = Next;

    public async Task Invoke(HttpContext Context)
    {
        // обработать входящий запрос

        await _Next(Context); // даём возможность поработать оставшейся части конвейера

        // дообработка результатов выполнения конвейера, хранимых в Context.Response
    }
}
