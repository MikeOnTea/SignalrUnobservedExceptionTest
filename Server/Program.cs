using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();
app.MapHub<MyHub>("/test");
app.Run();


class MyHub : Hub
{
    public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
    {
        while (true)
        {
            throw new Exception("Test");
            yield return DateTime.UtcNow;
            await Task.Delay(1000, cancellationToken);
        }
    }
}