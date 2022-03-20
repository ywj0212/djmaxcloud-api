using System.ComponentModel.DataAnnotations.Schema;

namespace DMRVAPI.Repositories.DataModel
{
    [Table("pattern_data")]
    public class PatternDataModel
    {
        public ushort? id { get; set; }
        public ushort? song_id { get; set; }
        public string? button { get; set; }
        public string? difficulty { get; set; }
        public byte? level { get; set; }
    }
}
