using DMRVAPI.Repositories.DataModel;
using Microsoft.EntityFrameworkCore;

namespace DMRVAPI.Repositories
{
    public partial class MariaDbContext : DbContext
    {
        public MariaDbContext(DbContextOptions<MariaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SongDataModel> SongDatas { get; set; }
        public virtual DbSet<PatternDataModel> PatternDatas { get; set; }
        public virtual DbSet<PerformanceDataModel> PerformanceDatas { get; set; }
        public virtual DbSet<PlayRecordDataModel> PlayRecordDatas { get; set; }
    }
}
