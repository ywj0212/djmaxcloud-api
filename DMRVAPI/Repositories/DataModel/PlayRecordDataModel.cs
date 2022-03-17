using System.ComponentModel.DataAnnotations.Schema;

namespace DMRVAPI.Repositories.DataModel
{
    [Table("play_record")]
    public record PlayRecordDataModel
    {
        public uint? id { get; init; }
        public uint? user_id { get; init; }
        public string? type { get; init; }
        public ushort? pattern_id { get; init; }
        public DateTime? register_date { get; init; }
        public string? speed { get; init; }
        public string? fever { get; init; }
        public string? fader { get; init; }
        public string? chaos { get; init; }
        public float? rate  { get; init; }
        public ushort? best_combo { get; init; }
        public ushort? max_100 { get; init; }
        public ushort? breaks { get; init; }
        public ushort? max_not_100 { get; init; }
        public ushort? max_90 { get; init; }
        public ushort? max_80 { get; init; }
        public ushort? max_70 { get; init; }
        public ushort? max_60 { get; init; }
        public ushort? max_50 { get; init; }
        public ushort? max_40 { get; init; }
        public ushort? max_30 { get; init; }
        public ushort? max_20 { get; init; }
        public ushort? max_10 { get; init; }
        public ushort? max_1 { get; init; }
    }
}
