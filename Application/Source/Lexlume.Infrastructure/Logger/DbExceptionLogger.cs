using Lexlume.Domain.Entities.Application;
using Lexlume.Domain.Interfaces;

namespace Lexlume.Infrastructure;

public class DbExceptionLogger(IDatabaseContext db) : IExceptionLogger
{
    public async Task LogExceptionAsync(Exception ex)
    {
        db.ErrorLogs.Add(new ErrorLog
        {
            Message = ex.Message,
            StackTrace = ex.StackTrace,
            InnerException = ex.InnerException?.Message
        });

        await db.SaveChangesAsync();
    }
}