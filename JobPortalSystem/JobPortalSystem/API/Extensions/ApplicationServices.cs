using Domain.Application.Features.Admin.Interfaces;
using Domain.Application.Features.Admin.Repositories;
using Domain.Application.Features.Admin.Services;
using Domain.Application.Features.Authuser.Interfaces;
using Domain.Application.Features.Authuser.Repositories;
using Domain.Application.Features.Authuser.Services;
using Domain.Application.Features.Job.Interfaces;
using Domain.Application.Features.Job.Repositories;
using Domain.Application.Features.Job.Services;
using Domain.Application.Features.JobProvider.Interfaces;
using Domain.Application.Features.JobProvider.Repositories;
using Domain.Application.Features.JobProvider.Services;
using Domain.Application.Features.JobSeeker.Interfaces;
using Domain.Application.Features.JobSeekers.Repositories;
using Domain.Application.Features.Login.Interfaces;
using Domain.Application.Features.Login.Repositories;
using Domain.Application.Features.Login.Services;
using Domain.Application.Features.Profile.Services;
using Domain.Application.Features.SignUp.In;
using Domain.Application.Features.SignUp.Interfaces;
using Domain.Application.Features.SignUp.Repositories;
using Domain.Application.Features.SignUp.Services;
using Domain.Infrastructure.ExternalServices;
using Domain.Models;
using Domain.Application.Features.User.Interfaces;
using Domain.Application.Features.User.Repositories;
using MailKit;
using Microsoft.EntityFrameworkCore;
using Domain.Application.Features.User.Services;


namespace JobPortalSystem.API.Extensions
{
    public static class ApplicationServices
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<JobPortalDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
            );
            services.AddScoped<IAdminServices, AdminServices>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddTransient<Domain.Infrastructure.ExternalServices.IMailService, EmailService>();
            services.AddScoped<ILoginRequestService, LoginRequestService>();
            services.AddScoped<ILoginRequestRepository, LoginRequestRepository>();

            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<IUserRepository, UserRepository>();
           
            services.AddScoped<IAuthUserRepository, AuthUserRepository>();
            services.AddScoped<IAuthUserService,AuthUserService>();

            services.AddScoped<ISignUpRequestService, SignUpRequestService>();
            services.AddScoped<ISignUpRequestRepository, SignUpRequestRepository>();

            services.AddScoped<IJobProviderService, JobProviderService>();
            services.AddScoped<IJobProviderRepository, JobProviderRepository>();

            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobServices, JobServices>();

            services.AddHttpContextAccessor();

            services.AddScoped<IInterviewService,InterviewService>();   
            services.AddScoped<IInterviewRepository,InterviewRepository>();

            services.AddScoped<IJobSeekerService, JobseekerService>();
            services.AddScoped<IJobSeekerRepository, JobSeekerRepository>();

            services.AddScoped<ICompanyRepository, Companyrepository>();
            services.AddScoped<ICompanyService,Companyservice>();  
         


            //         services.AddScoped<IChatRepository, ChatRepository>();
            //         services.AddScoped<IMessageGroupRepository, MessageGroupRepository>();


            return services;
        }
    }
}
