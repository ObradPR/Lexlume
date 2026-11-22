namespace Lexlume.Domain._Grid;

public class PagingResult<TEntity>
{
    public IEnumerable<TEntity> Data { get; set; }

    public long Total { get; set; }
}