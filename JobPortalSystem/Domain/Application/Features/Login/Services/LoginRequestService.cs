using AutoMapper;
using Domain.Application.Features.Authuser.Interfaces;
using Domain.Application.Features.Login.DTO;
using Domain.Application.Features.Login.Interfaces;
using Domain.Service.Login.DTOs;
using Domain.Application.Features.Authuser.Repositories;


namespace Domain.Application.Features.Login.Services
{
    public class LoginRequestService : ILoginRequestService
    {
        private readonly ILoginRequestRepository _loginRepository;

        private readonly IAuthUserRepository _authUserRepository;

        private readonly IMapper _mapper;

        public LoginRequestService(ILoginRequestRepository loginRepository,IAuthUserRepository authUserRepository,
            IMapper mapper)
        {
            _loginRepository = loginRepository;

            _authUserRepository = authUserRepository;

            _mapper = mapper;
        }

        //login
        public LoginResponseDto Login(LoginRequestDto request)
        {
            // CHECK USER

            var user =_loginRepository.GetUserByEmailpassword(request.Email, request.Password);

            if (user == null)
            {
                throw new Exception("Invalid Email or Password");
            }

            // MAP USER TO DTO

            var response =_mapper.Map<LoginResponseDto>(user);

            // GENERATE TOKEN

            response.Token =_authUserRepository.CreateToken(user);
            return response;
        }



    }
       
    }



