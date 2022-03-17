using System.ComponentModel.DataAnnotations.Schema;

namespace DMRVAPI.Repositories.DataModel
{
    [Table("pattern_data")]
    public record PatternDataModel
    {
        public ushort? id { get; init; }
        public ushort? song_id { get; init; }
        public string? button { get; init; }
        public string? difficulty { get; init; }
        public byte? level { get; init; }
    }
}
