using Microsoft.AspNetCore.Authorization;

namespace DMRVAPI
{
    public class Policies
    {
        public const string Admin = "Admin";
        public const string Moderator = "Moderator";
        public const string User = "User";

        public static AuthorizationPolicy AdminPolicy() => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        public static AuthorizationPolicy ModeratorPolicy() => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin, Moderator).Build();
        public static AuthorizationPolicy UserPolicy() => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin, Moderator, User).Build();
    }
}
