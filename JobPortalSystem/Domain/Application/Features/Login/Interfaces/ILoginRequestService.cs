using Domain.Application.Features.Login.DTO;
using Domain.Service.Login.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Application.Features.Login.Interfaces
{
    public interface ILoginRequestService
    {

        LoginResponseDto Login(LoginRequestDto request);



    }
}
