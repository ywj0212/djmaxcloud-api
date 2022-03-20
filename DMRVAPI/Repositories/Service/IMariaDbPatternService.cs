using DMRVAPI.Repositories.DataModel;

namespace DMRVAPI.Repositories.Service
{
    public interface IMariaDbPatternService
    {
        Task<IEnumerable<PatternDataModel>> GetList();
        Task<PatternDataModel?> Get(ushort id);
        Task<int> Insert(PatternDataModel pattern);
        Task<int> Insert(IEnumerable<PatternDataModel> patterns);
        Task<int> Update(PatternDataModel pattern);
        Task<int> Delete(ushort id);
    }
}