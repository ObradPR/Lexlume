using Microsoft.EntityFrameworkCore;

namespace Lexlume.Domain.Entities._Base.Interfaces;

public interface IConfigurableEntity
{
    void Configure(ModelBuilder builder);
}