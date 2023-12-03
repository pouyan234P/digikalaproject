using AutoMapper;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using IdentityModel;
using Digikala.Services.Identity.DTO;
using Digikala.Services.Identity.Helper;
using Digikala.Services.Identity.Models;
using Digikala.Services.Identity.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.Identity.Controllers
{

    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManger;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly IEventService _events;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IClientStore _clientStore;

        private string MyEmail { get; set; }
        public AccountController(IConfiguration configuration, UserManager<User> userManger, SignInManager<User> signInManager, RoleManager<Role> roleManager, IMailService mailService, IMapper mapper,IEventService events, IIdentityServerInteractionService interaction, IAuthenticationSchemeProvider schemeProvider, IClientStore clientStore)
        {

            _configuration = configuration;
            _userManger = userManger;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mailService = mailService;
            _mapper = mapper;
            _events = events;
            _interaction = interaction;
            _schemeProvider = schemeProvider;
            _clientStore = clientStore;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name should be provided.");
            }

            var newRole = new Role
            {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
            {
                return Ok();
            }

            return Problem(roleResult.Errors.First().Description, null, 500);
        }
        public async Task<IActionResult> AddUserToRole( Usertoroleandisactive usertoroleandisactive)
        {
            var user = _userManger.Users.Where(t => t.Email == usertoroleandisactive.email).Select(t => t).SingleOrDefault();

            var result = await _userManger.AddToRoleAsync(user, usertoroleandisactive.roleName);
            var t = await _userManger.FindByEmailAsync(usertoroleandisactive.email);
            user.EmailConfirmed = usertoroleandisactive.isActive;
            await _userManger.UpdateAsync(t);

            if (result.Succeeded)
            {
                return Ok();
            }
            return Problem(result.Errors.First().Description, null, 500);
        }
        [HttpGet]
        public async Task<IActionResult> Register(string returnUrl)
        {
            // build a model so we know what to show on the reg page
            var vm = await BuildRegisterViewModelAsync(returnUrl);

            return View(vm);
        }
        private async Task<RegisterDTO> BuildRegisterViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            List<string> roles = new List<string>();
            roles.Add("Customer");
            ViewBag.message = roles;
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == Duende.IdentityServer.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new RegisterDTO
                {
                    EnableLocalLogin = local,
                    Url = returnUrl,
                    Username = context?.LoginHint,
                };

                if (!local)
                {
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null)
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;
            if (context?.Client.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new RegisterDTO
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                Url = returnUrl,
                Username = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }
        [HttpPost]
        public async Task<IActionResult> Register( RegisterDTO registerDTO)
        {

            var usertocreate = new User
            {
                UserName = registerDTO.Name + registerDTO.family,
                Email = registerDTO.Email,
                Date = DateTime.Now.Date,
                EmailConfirmed = false,
                Country = registerDTO.Country,
                Name = registerDTO.Name,
                family = registerDTO.family
            };
            var result = await _userManger.CreateAsync(usertocreate, registerDTO.Password);

            MyEmail = registerDTO.Email;

            var createduser = await _userManger.FindByEmailAsync(MyEmail);
           /* WelcomeRequest welcomeRequest = new WelcomeRequest
            {
                UserName = createduser.UserName,
                ToEmail = createduser.Email,
                Registerurl = createduser.Url
            };
            await _mailService.SendWelcomeEmailAsync(welcomeRequest);*/
            return Redirect(registerDTO.Url);
        }
        [Authorize(Policy = "RequireAdminRole")]
        
        public async Task<IActionResult> RegisterAdminpanel( RegisterDTO registerDTO)
        {

            var usertocreate = new User
            {
                UserName = registerDTO.Name + registerDTO.family,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.phoneNumber.ToString(),
                Date = DateTime.Now.Date,
                Url = registerDTO.Url,
                EmailConfirmed = true,
                Country = registerDTO.Country,

            };
            var user = await _userManger.CreateAsync(usertocreate, registerDTO.Password);
            var Myuser = _userManger.Users.SingleOrDefault(u => u.Email == registerDTO.Email);
            var result = await _userManger.AddToRoleAsync(Myuser, registerDTO.Role);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = await BuildLoginViewModelAsync(returnUrl);

            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return RedirectToAction("Challenge", "External", new { scheme = vm.ExternalLoginScheme, returnUrl });
            }

            return View(vm);
        }
        private async Task<LoginInputModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == Duende.IdentityServer.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new LoginInputModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Email = context?.LoginHint,
                };

                if (!local)
                {
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null)
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;
            if (context?.Client.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginInputModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Email = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task<LoginInputModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Email = model.Email;
            return vm;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel userForLoginDTO)
        {
            var context = await _interaction.GetAuthorizationContextAsync(userForLoginDTO.ReturnUrl);
            if (ModelState.IsValid)
            {
                var user = await _userManger.Users.Where(t => t.Email == userForLoginDTO.Email).Select(t => t).Include(t => t.userRoles).ThenInclude(t => t.Role).FirstOrDefaultAsync();
                var result = await _signInManager.PasswordSignInAsync(user, userForLoginDTO.Password,true, lockoutOnFailure:false);

                if (result.Succeeded)
                {
                    
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.Name+" "+user.family));
                    return Redirect(userForLoginDTO.ReturnUrl);
                    /* return Ok(new
                     {
                        token = Generatejwt(user).Result

                     );*/
                }
                //next we will create token that use two information inside it

                return BadRequest("your user or login is incorrect");
            }
            return BadRequest("Something went wrong");
        }
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm);
            }

            return View(vm);
        }
        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }
            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            // build a model so the logged out page knows what to display
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await _signInManager.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }
            return View("LoggedOut", vm);
        }
            /*  public async Task<IActionResult> Login( UserForLoginDTO userForLoginDTO)
              {
                  var user = await _userManger.Users.Where(t => t.Email == userForLoginDTO.Email).Select(t => t).Include(t => t.userRoles).ThenInclude(t => t.Role).FirstOrDefaultAsync();
                  var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDTO.Password, false);

                  if (result.Succeeded)
                  {
                      return Ok(new
                      {
                          token = Generatejwt(user).Result
                      });
                  }
                  //next we will create token that use two information inside it

                  return BadRequest("your user or login is incorrect");
              }*/

            
           
        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != Duende.IdentityServer.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetUsers( UserParams userParams)
        {
            var users = _userManger.Users.Include(t => t.userRoles).ThenInclude(t => t.Role);
            /*  var users =  _userManger.Users.Include(t => t.userRoles).ThenInclude(t => t.Role).Select(u => new
              {
                  u.Id,
                  Username = u.UserName,
                  Email=u.Email,
                  PhoneNumber = u.PhoneNumber,
                  Country = u.Country,
                  Roles = u.userRoles.Select(r => r.Role.Name).ToList()
              });*/
            var t = await Pagedlist<User>.CreateAsync(users, userParams.PageNumber, userParams.pageSize);
            Response.AddPagination(t.CurrentPage, t.PageSize, t.TotalCount, t.TotalPage);

            return Ok(t);
        }
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userManger.FindByIdAsync(id.ToString());
            return Ok(user);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("DeleteUser/{userid}")]
        public async Task<IActionResult> DeleteUser(int userid)
        {
            string id = userid.ToString();
            var user = await _userManger.FindByIdAsync(id);
            await _userManger.DeleteAsync(user);
            return Ok();
        }
        [Authorize]
        public async Task<IActionResult> Updateuserfrommember(int userid,  RegisterDTO registerDTO)
        {
            string id = userid.ToString();
            var Myuser = await _userManger.FindByIdAsync(id);
            Myuser.Email = registerDTO.Email;
            Myuser.Country = registerDTO.Country;
            Myuser.PhoneNumber = registerDTO.phoneNumber;
            Myuser.UserName = registerDTO.Name + registerDTO.family;
            if (registerDTO.Password != null)
            {
                Myuser.PasswordHash = _userManger.PasswordHasher.HashPassword(Myuser, registerDTO.Password);

                await _userManger.UpdateAsync(Myuser);
            }
            else
                await _userManger.UpdateAsync(Myuser);
            return Ok();
        }


        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateUser(int userid,  RegisterDTO registerDTO)
        {
            string id = userid.ToString();
            var Myuser = await _userManger.FindByIdAsync(id);
            Myuser.Email = registerDTO.Email;
            Myuser.Country = registerDTO.Country;
            Myuser.PhoneNumber = registerDTO.phoneNumber.ToString();
            if (registerDTO.Password != null)
            {
                Myuser.PasswordHash = _userManger.PasswordHasher.HashPassword(Myuser, registerDTO.Password);
            }
            if (registerDTO.Role != null)
            {
                var resulte = _userManger.RemoveFromRoleAsync(Myuser, registerDTO.Role);
                var result = await _userManger.AddToRoleAsync(Myuser, registerDTO.Role);
                await _userManger.UpdateAsync(Myuser);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return Problem(result.Errors.First().Description, null, 500);
            }
            else
                await _userManger.UpdateAsync(Myuser);
            return Ok();
        }
        private async Task<string> Generatejwt(User user)
        {
            var claim = new List<Claim>
                        {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Actor,user.EmailConfirmed.ToString()),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var roles = await _userManger.GetRolesAsync(user);
            foreach (var item in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, item));
            }
            //we need a key to sing in our token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("appSetting:Token").Value));
            //we create signing credential
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //we going to create tokendescription to describe our expire date and signing credential
            var tokendescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = cred,

            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokendescription);
            return tokenhandler.WriteToken(token);
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmc = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmc.Key;
                passwordHash = hmc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
