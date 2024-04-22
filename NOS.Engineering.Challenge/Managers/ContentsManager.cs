using NOS.Engineering.Challenge.Database;
using NOS.Engineering.Challenge.Models;

namespace NOS.Engineering.Challenge.Managers;

public class ContentsManager : IContentsManager
{
    private readonly IDatabase<Content?, ContentDto> _database;

    public ContentsManager(IDatabase<Content?, ContentDto> database)
    {
        _database = database;
    }

    public Task<IEnumerable<Content?>> GetManyContents()
    {
        return _database.ReadAll();
    }

    public Task<Content?> CreateContent(ContentDto content)
    {
        return _database.Create(content);
    }

    public Task<Content?> GetContent(Guid id)
    {
        return _database.Read(id);
    }

    public Task<Content?> UpdateContent(Guid id, ContentDto content)
    {
        return _database.Update(id, content);
    }

    public Task<Guid> DeleteContent(Guid id)
    {
        return _database.Delete(id);
    }

    public Task<Content?> AddGenres(Guid id, IEnumerable<string> genres)
    {
        Content existingContent = this.GetContent(id);

        foreach (string genre in genres)
        {
            bool matches = existingContent.GenreList.Where(x => x.Genre == genre);

            if (matches)
            {
                // Already has this genre, remove from list and don't add
                genres.Remove(genre);
            }
        }

        ContentDto content = new()
        {
            GenreList = genres
        };

        return _database.AddGenres(id, content);
    }

    public Task<Content?> RemoveGenres(Guid id, IEnumerable<string> genres)
    {
        ContentDto content = new()
        {
            GenreList = genres
        };

        return _database.RemoveGenres(id, content);
    }

    public Task<IEnumerable<Content>> GetByTitleGenre(ContentDto content)
    {
        return _database.GetByTitleGenre(content);
    }
}