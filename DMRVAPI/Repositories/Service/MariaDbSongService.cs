using DMRVAPI.Repositories.DataModel;
using Microsoft.EntityFrameworkCore;

namespace DMRVAPI.Repositories.Service
{
    public class MariaDbSongService : IMariaDbSongService
    {
        private readonly MariaDbContext _dbContext;

        public MariaDbSongService(MariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<SongDataModel>> GetList()
        {
            return await _dbContext.SongDatas.ToListAsync();
        }
        public async Task<SongDataModel?> Get(ushort id)
        {
            return await _dbContext.SongDatas.FirstOrDefaultAsync(x => x.id == id);
        }
        public async Task<int> Insert(SongDataModel song)
        {
            _dbContext.Add(song);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> Update(SongDataModel song)
        {
            try
            {
                _dbContext.Update(song);
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }
        public async Task<int> Delete(ushort id)
        {
            try
            {
                _dbContext.SongDatas.Remove(
                    new SongDataModel
                    {
                        id = id
                    }
                );

                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }
    }
}
