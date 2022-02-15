using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAdvert.Web.Models.Accounts;

namespace WebAdvert.Web.Controllers
{

    /// <summary>
    /// Account controller for user auth using AWS
    /// Todo: Put logic in as service class
    /// </summary>
    public class Accounts : Controller
    {
        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly UserManager<CognitoUser> _userManager;
        private readonly CognitoUserPool _pool;

        public Accounts(SignInManager<CognitoUser> signInManager,
            UserManager<CognitoUser> userManager, CognitoUserPool pool)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _pool = pool;
        }

        public async Task<IActionResult> Signup()
        {
            var model = new SignupViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = _pool.GetUser(model.Email);
                if(user.Status != null)
                {
                    ModelState.AddModelError("UserExists","User with this email already exists");
                    return View(model);

                }

                user.Attributes.Add(
                    CognitoAttribute.Name.ToString(),
                    model.Email);

                //var createdUser = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
                
                //if (createdUser.Succeeded)
                if(true)
                {
                    return RedirectToAction("Confirm");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Confirm(ConfirmViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Confirm_Post(ConfirmViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("NotFound", "A user with a given email address is not found.");
                    return View(model);
                }

                //(_userManager as CognitoUserManager<CognitoUser>).ConfirmSignUpAsync();
                var result = await _userManager.ConfirmEmailAsync(user, model.Code).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
