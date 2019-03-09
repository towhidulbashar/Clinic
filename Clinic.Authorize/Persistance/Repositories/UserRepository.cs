using Clinic.Authorize.Core.Domain;
using Clinic.Authorize.Core.Repositories;
using Dapper;
using IdentityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Authorize.Persistance.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDbTransaction transaction) : base(transaction)
        {

        }
        public override Task Add(User entity)
        {
            throw new NotImplementedException();
        }

        public override void Add(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public async override Task<IEnumerable<User>> Get()
        {
            var patient = await Connection.QueryAsync<User>("SELECT * FROM user");
            return patient;
        }

        public override User Get(long id)
        {
            throw new NotImplementedException();
        }

        public override void Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public override User SingleOrDefault(Expression<Func<bool, User>> expression)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> ValidateCredentials(string username, string password)
        {
            string sql = @"SELECT EXISTS(SELECT 1 FROM userbase WHERE username = @username AND password= @password)";
            var credentialParameters = new DynamicParameters();
            credentialParameters.Add("username", username, DbType.String, ParameterDirection.Input, 64);
            credentialParameters.Add("password", password, DbType.String, ParameterDirection.Input, 1024);
            var isValid = await Connection
                .QueryFirstAsync<bool>(sql, credentialParameters);
            return isValid;
        }
        public Task<User> AutoProvisionUser(string provider, string userId, List<Claim> claims)
        {
            // create a list of claims that we want to transfer into our store
            var filtered = new List<Claim>();
            foreach (var claim in claims)
            {
                // if the external system sends a display name - translate that to the standard OIDC name claim
                if (claim.Type == ClaimTypes.Name)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, claim.Value));
                }
                // if the JWT handler has an outbound mapping to an OIDC claim use that
                else if (JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.ContainsKey(claim.Type))
                {
                    filtered.Add(new Claim(JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap[claim.Type], claim.Value));
                }
                // copy the claim as-is
                else
                {
                    filtered.Add(claim);
                }
            }

            // if no display name was provided, try to construct by first and/or last name
            if (!filtered.Any(x => x.Type == JwtClaimTypes.Name))
            {
                var first = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value;
                var last = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value;
                if (first != null && last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first + " " + last));
                }
                else if (first != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first));
                }
                else if (last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, last));
                }
            }

            // create a new unique subject id
            var sub = CryptoRandom.CreateUniqueId();
            // check if a display name is available, otherwise fallback to subject id
            var name = filtered.FirstOrDefault(c => c.Type == JwtClaimTypes.Name)?.Value ?? sub;
            // create new user
            var user = new User
            {
                SubjectId = sub,
                Username = name,
                ProviderName = provider,
                ProviderSubjectId = userId,
                Claims = filtered
            };

            // add user to in-memory store
            //_users.Add(user);

            return Task.FromResult(user);
        }
        public Task<User> FindByExternalProvider(string provider, string userId)
        {
            throw new NotImplementedException();
            /*  return _users.FirstOrDefault(x =>
                x.ProviderName == provider &&
                x.ProviderSubjectId == userId); */
        }
        public Task<User> FindBySubjectId(string subjectId)
        {
            throw new NotImplementedException();
            //return _users.FirstOrDefault(x => x.SubjectId == subjectId);
        }
        public Task<User> FindByUsername(string username)
        {
            throw new NotImplementedException();
            //return _users.FirstOrDefault(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
