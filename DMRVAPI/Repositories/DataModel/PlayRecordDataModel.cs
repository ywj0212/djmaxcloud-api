using System.ComponentModel.DataAnnotations.Schema;

namespace DMRVAPI.Repositories.DataModel
{
    [Table("play_record")]
    public class PlayRecordDataModel
    {
        public uint? id { get; set; }
        public uint? user_id { get; set; }
        public string? type { get; set; }
        public ushort? pattern_id { get; set; }
        public DateTime? register_date { get; set; }
        public string? speed { get; set; }
        public string? fever { get; set; }
        public string? fader { get; set; }
        public string? chaos { get; set; }
        public float? rate  { get; set; }
        public ushort? best_combo { get; set; }
        public ushort? max_100 { get; set; }
        public ushort? breaks { get; set; }
        public ushort? max_not_100 { get; set; }
        public ushort? max_90 { get; set; }
        public ushort? max_80 { get; set; }
        public ushort? max_70 { get; set; }
        public ushort? max_60 { get; set; }
        public ushort? max_50 { get; set; }
        public ushort? max_40 { get; set; }
        public ushort? max_30 { get; set; }
        public ushort? max_20 { get; set; }
        public ushort? max_10 { get; set; }
        public ushort? max_1 { get; set; }
    }
}
