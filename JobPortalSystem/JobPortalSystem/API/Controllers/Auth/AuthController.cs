using Domain.Application.Features.SignUp.DTO;
using Domain.Application.Features.SignUp.Interfaces;
using Domain.Application.Features.Login.DTO;
using Domain.Application.Features.Login.Interfaces;
using JobPortalSystem.API.Controllers.Auth.RequestObjects;
using JobPortalSystem.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Service.Login.DTOs;
using Domain.Application.Features.SignUp.In;

namespace JobPortalSystem.API.Controllers.Auth
{
    [ApiController]

    [Route("api/auth")]
    
    public class AuthController : BaseApiController<AuthController>
    {
        private readonly ISignUpRequestService _signUpService;
        private readonly ILoginRequestService _loginService;

        public AuthController
        (
            ISignUpRequestService signUpService,
            ILoginRequestService loginService
        )
        {
            _signUpService = signUpService;
            _loginService = loginService;
        }

        // SIGNUP
       

        [HttpPost("signup")]
        public async Task<IActionResult> Signup
        (
            [FromBody] SignUpRequestDto request
        )
        {
            try
            {
                await _signUpService .CreateSignupRequest(request);

                return Ok(new
                {
                    Message =   "Verification email sent successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // VERIFY EMAIL

        [HttpGet("verify-email/{signupId}")]
        public async Task<IActionResult> VerifyEmail
        (
            Guid signupId
        )
        {
            try
            {
                var result = await _signUpService .VerifyEmailAsync(signupId);

                if (result)
                {
                    return Ok(new
                    {
                        Message = "Email verified successfully"
                    });
                }

                return BadRequest(new
                {
                    Message = "Verification failed"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        // SET PASSWORD
       
        [HttpPost("set-password")]
        public async Task<IActionResult> SetPassword
        (
            [FromBody] SetPasswordRequest request
        )
        {
            try
            {
                await _signUpService .CreateUserAccount(
                        request.SignupId,
                        request.Password
                    );

                return Ok(new
                {
                    Message = "Account created successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        // LOGIN
        [HttpPost("login")]
        public IActionResult Login
        (
            [FromBody] LoginRequestDto request
        )
        {
            try
            {
                var response =
                    _loginService.Login(request );


                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
