using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Validation;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var userName = context.UserName;
        var password = context.Password;
        var request = context.Request;
        context.Result = new GrantValidationResult
        (
            subject: "subject",
            authenticationMethod: "custom",
            claims: new Claim[]
            {
                new Claim("Company", "Company Name")
            }
        );
        return;
    }
}