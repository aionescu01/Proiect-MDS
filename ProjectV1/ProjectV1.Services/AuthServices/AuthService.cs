using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectV1.DAL;
using ProjectV1.DAL.Entities.Auth;
using ProjectV1.DAL.Models.Auth;
using ProjectV1.DAL.Models.User;
using ProjectV1.DAL.Utils;

namespace ProjectV1.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly JWTHelper _jWTHelper;

        public AuthService(AppDbContext context, UserManager<User> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _jWTHelper = new JWTHelper(configuration);
        }

        #region refresh token setter
        private async Task<bool> SetAuthenticationTokenAsync(User user, string loginProvider, string name, string value)
        {
            if (user == null || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            {
                return false;
            }

            var existingToken = await _context.UserTokens.FirstOrDefaultAsync(x => x.UserId == user.Id
                                                                                && x.LoginProvider == loginProvider
                                                                                && x.Name == name);

            if (existingToken != null)
            {
                existingToken.Value = value;
            }
            else
            {
                var newToken = new IdentityUserToken<int>
                {
                    UserId = user.Id,
                    LoginProvider = loginProvider,
                    Name = name,
                    Value = value
                };
                await _context.UserTokens.AddAsync(newToken);
            }

            await _context.SaveChangesAsync();

            return true;
        }
        #endregion

        public async Task<UserAuthenticatedModel> Register(RegisterModel model)
        {
            if (model == null)
            {
                throw new ApplicationException("Model is null");
            }

            var userEmailCheck = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

            if (userEmailCheck != null)
            {
                throw new ApplicationException("Record with this email address already exists");
            }

            var user = new User()
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var saveResult = await _userManager.CreateAsync(user, model.Password);

            if (!saveResult.Succeeded)
            {
                int index = 0;
                var errors = saveResult.Errors.Select(x => new { x.Code, x.Description }).ToList();

                var errorsString = "";
                foreach (var error in errors)
                {
                    errorsString += $"nr: {index}, code: {error.Code}, description: {error.Description}/n";
                    index++;
                }
                throw new ApplicationException(errorsString);
            }

            await _userManager.AddToRoleAsync(user, "General");

            var roles = new List<string> { "General" };

            var accessToken = _jWTHelper.GenerateJwtToken(user, roles, null);
            var refreshToken = _jWTHelper.GenerateRefreshToken();

            await SetAuthenticationTokenAsync(user, "", "RefreshToken", refreshToken);

            var result = new UserAuthenticatedModel()
            {
                UserId = user.Id,
                AccessToken = accessToken,
                FirstName = model.FirstName,
                LastName = model.LastName,
                RefreshToken = refreshToken,
                Email = model.Email,
                Roles = roles,
                Permissions = new List<string>()
            };

            return result;
        }

        public async Task<UserAuthenticatedModel> Login(LoginModel model)
        {
            if (model == null)
            {
                throw new ApplicationException("Model is null");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
            {
                throw new ApplicationException("Email not registered on platform");
            }

            var checkResult = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkResult)
            {
                throw new ApplicationException("Could not login!");
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = (await _userManager.GetRolesAsync(user)).ToList();
            var accessToken = _jWTHelper.GenerateJwtToken(user, roles, userClaims);

            var refreshToken = _jWTHelper.GenerateRefreshToken();
            var setRefreshTokenResult = await SetAuthenticationTokenAsync(user, "", "RefreshToken", refreshToken);

            if (!setRefreshTokenResult)
            {
                throw new ApplicationException("Could not login");
            }

            var result = new UserAuthenticatedModel
            {
                UserId = user.Id,
                AccessToken = accessToken,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = model.Email,
                RefreshToken = refreshToken,
                Roles = roles,
                Permissions = userClaims.Select(d => d.Value).ToList()
            };

            return result;
        }
    }
}
