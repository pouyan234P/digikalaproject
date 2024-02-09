using AutoMapper;
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
using Digikala.Services.Identity.DbContexts;

namespace Mango.Services.Identity.Controllers
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        protected ResponseDTO _response;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManger;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDb;

        private string MyEmail { get; set; }

        public AccountController(IConfiguration configuration, UserManager<User> userManger, SignInManager<User> signInManager, RoleManager<Role> roleManager, IMailService mailService, IMapper mapper, ApplicationDbContext applicationDb)
        {

            _configuration = configuration;
            _userManger = userManger;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mailService = mailService;
            _mapper = mapper;
            _applicationDb = applicationDb;
            this._response = new ResponseDTO();
        }
        [HttpPost("CreateRole/{roleName}")]
        [Authorize(Policy = "RequireAdminRole")]
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
        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole([FromBody]Usertoroleandisactive usertoroleandisactive)
        {
            var user = _userManger.Users.Where(t => t.Email == usertoroleandisactive.email).Select(t => t).SingleOrDefault();

            var result = await _userManger.AddToRoleAsync(user, usertoroleandisactive.roleName);
            var t = await _userManger.FindByEmailAsync(usertoroleandisactive.email);
            t.EmailConfirmed = usertoroleandisactive.isActive;
            await _userManger.UpdateAsync(t);

            if (result.Succeeded)
            {
                return Ok();
            }
            return Problem(result.Errors.First().Description, null, 500);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {

            var usertocreate = new User
            {
                UserName = registerDTO.Name + registerDTO.family,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.phoneNumber.ToString(),
                Date = DateTime.Now.Date,
                Url = "http://localhost:4200/verfication/" + registerDTO.Email,
                EmailConfirmed = false,
                Country = registerDTO.Country,
                Name = registerDTO.Name,
                family = registerDTO.family
            };
            var result = await _userManger.CreateAsync(usertocreate, registerDTO.Password);

            MyEmail = registerDTO.Email;

            var createduser = await _userManger.FindByEmailAsync(MyEmail);
            WelcomeRequest welcomeRequest = new WelcomeRequest
            {
                UserName = createduser.UserName,
                ToEmail = createduser.Email,
                Registerurl = createduser.Url
            };
            await _mailService.SendWelcomeEmailAsync(welcomeRequest);
            return Ok();
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("RegisterAdminpanel")]
        public async Task<IActionResult> RegisterAdminpanel([FromBody] RegisterDTO registerDTO)
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
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDTO userForLoginDTO)
        {
            var user = await _userManger.Users.Where(t => t.Email == userForLoginDTO.Email).Select(t => t).Include(t => t.userRoles).ThenInclude(t => t.Role).FirstOrDefaultAsync();
            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDTO.Password, false);
            if (result.Succeeded)
            {
                string token = Generatejwt(user).Result;
                _response.Result = token;
                return Ok( _response);
            }
            //next we will create token that use two information inside it

            return BadRequest("your user or login is incorrect");
        }
            //next we will create token that use two information inside it

           
        
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers([FromQuery] UserParams userParams)
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
        [HttpGet("GetUser/{id}")]
        [Authorize]
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
        [HttpPost("Updateuserfrommember/{userid}")]
        public async Task<IActionResult> Updateuserfrommember(int userid, [FromBody] RegisterDTO registerDTO)
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
        [HttpPost("UpdateUser/{userid}")]
        public async Task<IActionResult> UpdateUser(int userid, [FromBody] RegisterDTO registerDTO)
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
        public string[] convertarraytobyte(byte[] myobj)
        {
            byte[] bytes = myobj;
            string oneBigString = Encoding.ASCII.GetString(bytes);
            string[] lines = oneBigString.Split('\n');
            return lines;
        }
    }
}
