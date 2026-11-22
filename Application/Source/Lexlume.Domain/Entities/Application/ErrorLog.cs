using Lexlume.Domain.Entities._Base;
using Lexlume.Domain.Entities._Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lexlume.Domain.Entities.Application;

public class ErrorLog : BaseDomain<long>, ILoggerEntity, IConfigurableEntity
{
    public string Message { get; set; }

    public string StackTrace { get; set; }

    public string InnerException { get; set; }

    public DateTime RecordDate { get; set; }

    //
    // Configuration
    //

    public void Configure(ModelBuilder builder)
    {
        builder.Entity<ErrorLog>(_ =>
        {
            _.Property(_ => _.Message).IsRequired();
        });
    }
}