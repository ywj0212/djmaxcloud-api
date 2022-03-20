using System.ComponentModel.DataAnnotations.Schema;

namespace DMRVAPI.Repositories.DataModel
{
    [Table("user_data")]
    public class UserDataModel
    {
        public uint? id { get; set; }
        public ulong? steam_id { get; set; }
        public bool? private_flag { get; set; }
        public DateTime? last_update { get; set; }
        public string? steam_profile { get; set; }
        public string? steam_friends { get; set; }
        public DateTime? steam_last_update { get; set; }
        public string? role { get; set; }
    }
}
