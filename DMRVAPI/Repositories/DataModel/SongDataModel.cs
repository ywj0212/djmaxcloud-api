using System.ComponentModel.DataAnnotations.Schema;

namespace DMRVAPI.Repositories.DataModel
{
    [Table("song_data")]
    public record SongDataModel
    {
        public ushort? id { get; init; }
        public string? category { get; init; }
        public string? title { get; init; }
        public string? composer { get; init; }
        public ushort? min_bpm { get; init; }
        public ushort? max_bpm { get; init; }
        public ushort? duration { get; init; }
        public bool? copyright { get; init; }
        public string? bga_url { get; init; }
        public string? hidden_bga_url { get; init; }
        public string? thumbnail_img { get; init; }
        public string? preview_img { get; init; }
        public string? loading_img { get; init; }

    }
}
