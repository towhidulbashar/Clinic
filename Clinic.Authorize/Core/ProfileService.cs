/* using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;

public class ProfileService : IProfileService
{
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.FindFirst("sub")?.Value;
        if (sub != null)
        {
            var user = "";
            var claimsPrinciple = await GetClaims(user);
            var claims = claimsPrinciple.Claims;
            if (context.RequestedClaimTypes != null && context.RequestedClaimTypes.Any())
            {
                claims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type));
            }
            context.IssuedClaims = claims.ToList();
        }
    }
    public Task IsActiveAsync(IsActiveContext context)
    {
        return Task.FromResult(0);
    }
    private async Task<ClaimsPrincipal> GetClaims(string user)
    {
        if(user == null)
            throw new ArgumentNullException(nameof(user));
        var id = new ClaimsIdentity();
        id.AddClaim(new Claim(JwtClaimTypes.PreferredUserName, user));
        return new ClaimsPrincipal(id);
    }
} */