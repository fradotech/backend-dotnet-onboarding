using System.Threading.Channels;
using Iam.Models;

public class QueueService
{
    private readonly Channel<string> _channel;

    public QueueService()
    {
        _channel = Channel.CreateUnbounded<string>();
    }

    public async Task SendMessageAsync(string message, User user)
    {
        Console.WriteLine($"QueueService: Sending message '{message}' to {user.Name}");

        await _channel.Writer.WriteAsync(message);
    }

    public IAsyncEnumerable<string> ReadAllAsync()
    {
        return _channel.Reader.ReadAllAsync();
    }
}