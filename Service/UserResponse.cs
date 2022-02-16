using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;

namespace WebAdvert.Web.Service
{
    public class UserResponse
    {
        public IdentityResult IdentityResult { get; set; }
        public CognitoUser User { get; set; }
        public bool Success { get; set; }
    }
}
