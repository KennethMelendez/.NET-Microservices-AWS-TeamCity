using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace WebAdvert.Web.Service
{
    public class UserCognitoRepository : IUserCognitoRepository
    {
        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly UserManager<CognitoUser> _userManager;
        private readonly CognitoUserPool _pool;

        public UserCognitoRepository(SignInManager<CognitoUser> signInManager,
            UserManager<CognitoUser> userManager, CognitoUserPool pool)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _pool = pool;
        }

        public async Task<UserResponse> CreateUserAsync(CognitoUser user, string password)
        {

            UserResponse userResponse= new UserResponse();
            var createdUser = await _userManager.CreateAsync(user, password).ConfigureAwait(false);

            if (createdUser != null)
            {
                userResponse.IdentityResult = createdUser;
                userResponse.Success = true;
            }
            else
            {
                userResponse.Success = false;
            }

            return userResponse;
        }

        public async Task<UserResponse> FindUserByEmail(string email)
        {
            UserResponse userResponse = new UserResponse();
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                userResponse.Success = true;
                userResponse.User = user;
            }
            else
            {
                userResponse.Success = false;
            }
            return userResponse;
        }
    }
}
