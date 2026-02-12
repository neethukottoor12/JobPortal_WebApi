
//using Domain.Models;
using Domain.Models;
using Domain.Service.Authuser.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Domain.Service.Authuser
{
	public class AuthUserService: IAuthUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IAuthUserRepository _userRepository;

		public AuthUserService(IHttpContextAccessor httpContextAccessor, IAuthUserRepository userRepository)
		{
			_httpContextAccessor = httpContextAccessor;
			_userRepository = userRepository;
		}
        public string GetUserId()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value.ToString();
            }
            return result;
        }
        public async Task<CompanyUser> GetUser(Guid userid)
        {
            return await _userRepository.GetUser(userid);
        }



    }
}
