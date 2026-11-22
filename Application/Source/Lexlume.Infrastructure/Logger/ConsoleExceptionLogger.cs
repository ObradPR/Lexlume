using Lexlume.Domain.Interfaces;
using Serilog;

namespace Lexlume.Infrastructure;

public class ConsoleExceptionLogger : IExceptionLogger
{
    public Task LogExceptionAsync(Exception ex)
    {
        Log.Error(ex, $"An Error occurred\nMessage: {ex.Message}");
        return Task.CompletedTask;
    }
}