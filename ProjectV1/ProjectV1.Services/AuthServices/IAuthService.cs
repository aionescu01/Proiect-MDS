using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectV1.DAL.Models.Auth;
using ProjectV1.DAL.Models.User;

namespace ProjectV1.Services.AuthServices
{
    public interface IAuthService
    {
        Task<UserAuthenticatedModel> Register(RegisterModel model);
        Task<UserAuthenticatedModel> Login(LoginModel model);
    }
}
