using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;

namespace Clinic.Authorize
{
    public class Config
    {
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser> 
                { 
                    new TestUser
                    {                        
                         SubjectId = "123456",
                         Username = "Alice",
                         Password = "Alice",
                         Claims = {
                             new Claim(JwtClaimTypes.Name, "Alice Smith"),
                             new Claim(JwtClaimTypes.Email, "alice@emai.com")
                         }
                         /* Claims = {
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                            new Claim(JwtClaimTypes.Address, 
                                @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", 
                                IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                        } */
                    }
                };
        }
    }
}