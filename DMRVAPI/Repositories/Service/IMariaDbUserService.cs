using DMRVAPI.Repositories.DataModel;

namespace DMRVAPI.Repositories.Service
{
    public interface IMariaDbUserService
    {
        Task<UserDataModel?> GetViaId(uint id);
        Task<UserDataModel?> GetViaSteamId(ulong steam64Id);
        Task<int> UpdateSteamInfo(ulong steam64Id);
        Task<int> Insert(UserDataModel user);
        Task<int> Update(UserDataModel user);
        Task<int> DeleteViaId(uint id);
        Task<int> DeleteViaSteamId(ulong steam64Id);
    }
}