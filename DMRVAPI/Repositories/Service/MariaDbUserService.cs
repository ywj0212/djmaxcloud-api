using DMRVAPI.Models;
using DMRVAPI.Repositories.DataModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DMRVAPI.Repositories.Service
{
    public class MariaDbUserService : IMariaDbUserService
    {
        private readonly MariaDbContext _context;
        private readonly IConfiguration _configuration;

        public MariaDbUserService(MariaDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UserDataModel?> GetViaId(uint id)
        {
            return await _context.UserDatas.FirstOrDefaultAsync(x => x.id == id);
        }
        public async Task<UserDataModel?> GetViaSteamId(ulong steam64Id)
        {
            return await _context.UserDatas.FirstOrDefaultAsync(x => x.steam_id == steam64Id);
        }
        public async Task<int> UpdateSteamInfo(ulong steam64Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"http://api.steampowered.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string steamApiKey = _configuration["ConnectionStrings:SteamApiKey"];

                // HTTP GET
                var profileRequest = client.GetAsync($"ISteamUser/GetPlayerSummaries/v0002/?key={steamApiKey}&steamids={steam64Id}&format=json");
                var friendsRequest = client.GetAsync($"ISteamUser/GetFriendList/v0001/?key={steamApiKey}&steamid={steam64Id}&relationship=friend&format=json");

                await Task.WhenAll(profileRequest, friendsRequest);

                var profileResult = await profileRequest.Result.Content.ReadAsStringAsync();
                var friendsResult = await friendsRequest.Result.Content.ReadAsStringAsync();

                SteamProfile steamProfile = JsonConvert.DeserializeObject<SteamProfile>(profileResult)!;
                SteamFriendsList steamFriends = JsonConvert.DeserializeObject<SteamFriendsList>(friendsResult)!;

                string friendSteamIds = string.Join(',', steamFriends.friendslist.friends.Select(x => x.steamid));
                var friendsProfileRequest = client.GetAsync($"ISteamUser/GetPlayerSummaries/v0002/?key={steamApiKey}&steamids={friendSteamIds}&format=json");
                var friendsProfileResult = await friendsProfileRequest.Result.Content.ReadAsStringAsync();

                SteamProfile friendsSteamProfile = JsonConvert.DeserializeObject<SteamProfile>(friendsProfileResult)!;

                var profile = JsonConvert.SerializeObject(steamProfile.response.players.FirstOrDefault()!);
                var friends = JsonConvert.SerializeObject(friendsSteamProfile.response.players);

                var userCtx = _context.UserDatas.Where(x => x.steam_id == steam64Id).FirstOrDefault()!;
                if (!string.IsNullOrEmpty(profile))
                {
                    userCtx.steam_profile = profile;
                    userCtx.steam_last_update = DateTime.Now;
                }
                if(!string.IsNullOrEmpty(friends))
                {
                    userCtx.steam_friends = friends;
                    userCtx.steam_last_update = DateTime.Now;
                }

                return await _context.SaveChangesAsync();
            }
        }
        public async Task<int> Insert(UserDataModel user)
        {
            _context.UserDatas.Add(user);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Update(UserDataModel user)
        {
            try
            {
                _context.UserDatas.Update(user);
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }
        public async Task<int> DeleteViaId(uint id)
        {
            try
            {
                _context.UserDatas.Remove(
                    new UserDataModel
                    {
                        id = id
                    }
                );

                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }
        public async Task<int> DeleteViaSteamId(ulong steam64Id)
        {
            try
            {
                _context.UserDatas.Remove(
                    new UserDataModel
                    {
                        steam_id = steam64Id
                    }
                );

                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }
    }
}
