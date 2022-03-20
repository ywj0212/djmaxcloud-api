using System.ComponentModel.DataAnnotations.Schema;

namespace DMRVAPI.Repositories.DataModel
{
    [Table("song_data")]
    public class SongDataModel
    {
        public ushort? id { get; set; }
        public string? category { get; set; }
        public string? title { get; set; }
        public string? composer { get; set; }
        public ushort? min_bpm { get; set; }
        public ushort? max_bpm { get; set; }
        public ushort? duration { get; set; }
        public bool? copyright { get; set; }
        public string? bga_url { get; set; }
        public string? hidden_bga_url { get; set; }
        public string? thumbnail_img { get; set; }
        public string? preview_img { get; set; }
        public string? loading_img { get; set; }

    }
}
