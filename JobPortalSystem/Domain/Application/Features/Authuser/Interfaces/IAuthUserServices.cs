using Domain.Application.Features.Authuser.Repositories;
using Domain.Application.Features.Authuser.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.Authuser.Interfaces
{
    public interface IAuthUserService
    {
        string GetUserId();
        CompanyUser GetUser(Guid userid);

    }
}
