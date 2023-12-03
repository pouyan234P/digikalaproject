using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Digikala.Services.Identity.DbContexts;
using Digikala.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Digikala.Services.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly UserManager<User> _userManger;
        private readonly RoleManager<Role> _roleManager;

        public ProfileService(IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,UserManager<User> userManger,RoleManager<Role> roleManager)
        {
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _userManger = userManger;
            _roleManager = roleManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            User user = await _userManger.FindByIdAsync(sub);
            ClaimsPrincipal userclaims = await _userClaimsPrincipalFactory.CreateAsync(user);
            List<Claim> claims = userclaims.Claims.ToList();
            claims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
          //  claims.Add(new Claim(JwtClaimTypes.FamilyName, user.family));
            //claims.Add(new Claim(JwtClaimTypes.Name, user.Name));
           // claims.Add(new Claim(JwtClaimTypes.Email, user.Email));
            if (_userManger.SupportsUserRole)
            {
                IList<string> roles = await _userManger.GetRolesAsync(user);
                foreach(var rolename in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, rolename));
                    if (_roleManager.SupportsRoleClaims)
                    {
                        Role role = await _roleManager.FindByNameAsync(rolename);
                        if(role!=null)
                        {
                            claims.AddRange(await _roleManager.GetClaimsAsync(role));
                        }
                    }
                }
            }
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            User user = await _userManger.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
