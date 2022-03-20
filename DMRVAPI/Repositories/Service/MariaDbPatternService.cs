using DMRVAPI.Repositories.DataModel;
using Microsoft.EntityFrameworkCore;

namespace DMRVAPI.Repositories.Service
{
    public class MariaDbPatternService : IMariaDbPatternService
    {
        private readonly MariaDbContext _context;

        public MariaDbPatternService(MariaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PatternDataModel>> GetList()
        {
            return await _context.PatternDatas.ToListAsync();
        }
        public async Task<PatternDataModel?> Get(ushort id)
        {
            return await _context.PatternDatas.FirstOrDefaultAsync(x => x.id == id);
        }
        public async Task<int> Insert(PatternDataModel pattern)
        {
            _context.Add(pattern);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Insert(IEnumerable<PatternDataModel> patterns)
        {
            _context.AddRange(patterns);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Update(PatternDataModel pattern)
        {
            try
            {
                _context.Update(pattern);
                return await _context.SaveChangesAsync();
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
                _context.PatternDatas.Remove(
                    new PatternDataModel
                    {
                        id = id
                    }
                );

                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

    }
}
