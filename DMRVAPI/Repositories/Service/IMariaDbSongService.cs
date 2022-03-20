using DMRVAPI.Repositories.DataModel;

namespace DMRVAPI.Repositories.Service
{
    public interface IMariaDbSongService
    {
        Task<IEnumerable<SongDataModel>> GetList();
        Task<SongDataModel?> Get(ushort id);
        Task<int> Insert(SongDataModel song);
        Task<int> Update(SongDataModel song);
        Task<int> Delete(ushort id);
    }
}