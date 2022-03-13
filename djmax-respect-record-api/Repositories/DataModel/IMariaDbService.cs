
namespace DMRVAPI.Repositories.DataModel
{
    public interface IMariaDbService
    {
        Task<IEnumerable<SongDataModel>> GetSongs();
        Task<SongDataModel?> GetSong(ushort id);
        Task<int> InsertSong(SongDataModel song);
        Task<int> UpdateSong(SongDataModel song);
        Task<int> DeleteSong(ushort id);

        Task<IEnumerable<PatternDataModel>> GetPatterns();
        Task<PatternDataModel?> GetPattern(ushort id);
        Task<int> InsertPattern(PatternDataModel pattern);
        Task<int> UpdatePattern(PatternDataModel pattern);
        Task<int> DeletePattern(ushort id);
    }
}