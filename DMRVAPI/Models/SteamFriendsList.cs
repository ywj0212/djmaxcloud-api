namespace DMRVAPI.Models
{
    public class SteamFriendsList
    {
        public FriendsList friendslist = new FriendsList();
        public class FriendsList
        {
            public List<Friend> friends = new List<Friend>();
            public class Friend
            {
                public string? steamid;
                // public string? relationship;
                // public int friend_since;
            }
        }
    }
}
