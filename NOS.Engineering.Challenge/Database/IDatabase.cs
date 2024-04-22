namespace NOS.Engineering.Challenge.Database;

public interface IDatabase<TOut, in TIn>
{
    Task<TOut?> Create(TIn item);
    Task<TOut?> Read(Guid id);
    Task<IEnumerable<TOut?>> ReadAll();
    Task<TOut?> Update(Guid id, TIn item);
    Task<Guid> Delete(Guid id);
    Task<TOut?> AddGenres(Guid id, TIn item);
    Task<TOut?> RemoveGenres(Guid id, TIn item);
    Task<IEnumerable<TOut?>> GetByTitleGenre(TIn item);
}