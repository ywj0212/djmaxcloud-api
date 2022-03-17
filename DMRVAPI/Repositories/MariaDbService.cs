using Microsoft.EntityFrameworkCore;

namespace DMRVAPI.Repositories.DataModel
{
    public class MariaDbService : IMariaDbService
    {
        private readonly MariaDbContext _dbContext;

        public MariaDbService(MariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<SongDataModel>> GetSongs()
        {
            return await _dbContext.SongDatas.ToListAsync();
        }
        public async Task<SongDataModel?> GetSong(ushort id)
        {
            return await _dbContext.SongDatas.FirstOrDefaultAsync(x => x.id == id);
        }
        public async Task<int> InsertSong(SongDataModel song)
        {
            _dbContext.Add(song);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> UpdateSong(SongDataModel song)
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
        public async Task<int> DeleteSong(ushort id)
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

        public async Task<IEnumerable<PatternDataModel>> GetPatterns()
        {
            return await _dbContext.PatternDatas.ToListAsync();
        }
        public async Task<PatternDataModel?> GetPattern(ushort id)
        {
            return await _dbContext.PatternDatas.FirstOrDefaultAsync(x => x.id == id);
        }
        public async Task<int> InsertPattern(PatternDataModel pattern)
        {
            _dbContext.Add(pattern);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> UpdatePattern(PatternDataModel pattern)
        {
            try
            {
                _dbContext.Update(pattern);
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }
        public async Task<int> DeletePattern(ushort id)
        {
            try
            {
                _dbContext.PatternDatas.Remove(
                    new PatternDataModel
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
