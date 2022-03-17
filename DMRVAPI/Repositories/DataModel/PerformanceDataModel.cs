using System.ComponentModel.DataAnnotations.Schema;

namespace DMRVAPI.Repositories.DataModel
{
    [Table("performance_data")]
    public record PerformanceDataModel
    {
        public uint? id { get; init; }
        public uint? user_id { get; init; }
        public ushort? pattern_id { get; init; }
        public DateTime? register_date { get; init; }
        public float? rate { get; init; }
        public ushort? breaks { get; init; }
    }
}
