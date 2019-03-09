using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Clinic.Authorize.Core.Domain;

namespace Clinic.Authorize.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> ValidateCredentials(string username, string password);
        Task<User> FindBySubjectId(string subjectId);
        Task<User> FindByUsername(string username);
        Task<User> FindByExternalProvider(string provider, string userId);
        Task<User> AutoProvisionUser(string provider, string userId, List<Claim> claims);
    }
}
