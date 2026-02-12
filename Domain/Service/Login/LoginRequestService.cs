using AutoMapper;
using Domain.Service.Authuser.DTOs;
using Domain.Service.Authuser.Interfaces;
using Domain.Service.Login.DTOs;
using Domain.Service.Login.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Login
{
    public class LoginRequestService : ILoginRequestService
    {
        ILoginRequestRepository jobSeekerRepository;
        IAuthUserRepository authUserRepository;
        IMapper mapper;
        public LoginRequestService(ILoginRequestRepository _jobSeekerRepository, IAuthUserRepository _authUserRepository, IMapper _mapper)
        {
            jobSeekerRepository = _jobSeekerRepository;
            authUserRepository = _authUserRepository;
            mapper = _mapper;
        }
        public async Task<JobSeekerLoginDto> login(string email, string password)
        {
            var user = await jobSeekerRepository.GetUserByEmailpassword(email, password);
            if (user == null)
            {
                return null;
            }
            else
            {
                    var userReturn = mapper.Map<JobSeekerLoginDto>(user);
                    userReturn.Token = authUserRepository.CreateToken(user);
                    return userReturn;
               
            }
        }
        public async Task<JobProviderLoginDto> loginJP(string email, string password)
        {
            var user = await jobSeekerRepository.GetUserByEmailpasswordJp(email, password);

            if (user == null)
            {
                return null;
            }
            else
            {
               
                    
                    var userReturn = mapper.Map<JobProviderLoginDto>(user);
                    userReturn.Token = authUserRepository.CreateToken(user);
                    return userReturn;
              
            }
        }
    }
}
