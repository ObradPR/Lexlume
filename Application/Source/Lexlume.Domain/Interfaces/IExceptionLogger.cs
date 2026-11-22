namespace Lexlume.Domain.Interfaces;

public interface IExceptionLogger
{
    Task LogExceptionAsync(Exception ex);
}