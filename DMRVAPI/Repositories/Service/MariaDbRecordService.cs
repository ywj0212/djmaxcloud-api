using DMRVAPI.Repositories.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DMRVAPI.Repositories.Service
{
    public class MariaDbRecordService : IMariaDbRecordService
    {
        private readonly MariaDbContext _dbContext;

        public MariaDbRecordService(MariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<PlayRecordDataModel>> GetRecent(int count, int page)
        {
            return await _dbContext.PlayRecordDatas.Skip(page * count).Take(count).ToArrayAsync();
        }
        public async Task<PlayRecordDataModel?> Get(uint id)
        {
            return await _dbContext.PlayRecordDatas.FirstOrDefaultAsync(x => x.id == id);
        }
        public async Task<int> Insert(PlayRecordDataModel record)
        {
            _dbContext.Add(record);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> Insert(IEnumerable<PlayRecordDataModel> records)
        {
            _dbContext.AddRange(records);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> Update(PlayRecordDataModel record)
        {
            try
            {
                _dbContext.Update(record);
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }
        public async Task<int> Delete(uint id)
        {
            try
            {
                _dbContext.PlayRecordDatas.Remove(
                    new PlayRecordDataModel
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
