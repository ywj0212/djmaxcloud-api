using DMRVAPI.Repositories.DataModel;

namespace DMRVAPI.Repositories.Service
{
    public interface IMariaDbRecordService
    {
        Task<IEnumerable<PlayRecordDataModel>> GetRecent(int count, int page);
        Task<PlayRecordDataModel?> Get(uint id);
        Task<int> Insert(PlayRecordDataModel record);
        Task<int> Insert(IEnumerable<PlayRecordDataModel> records);
        Task<int> Update(PlayRecordDataModel record);
        Task<int> Delete(uint id);
    }
}