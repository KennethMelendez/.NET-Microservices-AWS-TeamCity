using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace WebAdvert.Web.Service
{
    public interface IUserCognitoRepository
    {
        public Task<UserResponse> CreateUserAsync(CognitoUser user, string code);

        public Task<UserResponse> FindUserByEmail(string email);
    }
}
