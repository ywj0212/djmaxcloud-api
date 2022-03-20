namespace DMRVAPI.Models
{
    public class SteamProfile
    {
        public Response response = new Response();
        public class Response
        {
            public List<Player> players = new List<Player>();
            public class Player
            {
                public string? steamid;
                // public int communityvisibilitystate;
                // public int profilestate;
                public string? personaname;
                // public int commentpermission;
                public string? profileurl;
                public string? avatar;
                public string? avatarmedium;
                public string? avatarfull;
                // public string? avatarhash;
                // public int lastlogoff;
                // public int personastate;
                // public string? realname;
                // public string? primaryclanid;
                // public int timecreated;
                // public int personastateflags;
                public string? loccountrycode;
                // public string? locstatecode;
            }
        }
    }
}
