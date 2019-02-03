using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
public class AccountController: Controller
{
    public async Task<IActionResult> Logout()
    {
        /* await HttpContext.SignOutAsync("oidc");
        return Redirect(" "); */
        return SignOut(new AuthenticationProperties
        {
            RedirectUri = ""
        }, "oidc");
    }    
}