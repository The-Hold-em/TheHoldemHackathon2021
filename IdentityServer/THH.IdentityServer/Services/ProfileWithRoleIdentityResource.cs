using Duende.IdentityServer.Models;

using IdentityModel;

namespace THH.IdentityServer.Services
{
    public class ProfileWithRoleIdentityResource : IdentityResources.Profile
    {
        public ProfileWithRoleIdentityResource()
        {
            UserClaims.Add(JwtClaimTypes.Role);
        }
    }
}
