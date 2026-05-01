using Domain.Helpers;
using Domain.Models;
using Domain.Service;
using Domain.Service.Admin;
using Domain.Service.Admin.Interfaces;
using Domain.Service.Authuser;
using Domain.Service.Authuser.Interfaces;
using Domain.Service.Job;
using Domain.Service.Job.Interfaces;
using Domain.Service.JobProvider;
using Domain.Service.JobProvider.Interfaces;
using Domain.Service.Login;
using Domain.Service.Login.Interfaces;
using Domain.Service.Profile;
using Domain.Service.Profile.Intefaces;
using Domain.Service.SignUp;
using Domain.Service.SignUp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobPortal_Project.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DbHireMeNowWebApiContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.MigrationsAssembly("Domain"))
            );
            services.AddScoped<IAuthUserRepository, AuthUserRepository>();
            services.AddScoped<IJobProviderService, JobProviderService>();
            services.AddScoped<IJobProviderRepository, JobProviderRepository>();
            services.AddScoped<IEMailService, EmailService>();
            services.AddScoped<ILoginRequestService, LoginRequestService>();
            services.AddScoped<ILoginRequestRepository, LoginRequestRepository>();
            services.AddScoped<ICompanyService, Companyservice>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IInterviewService, InterviewService>();
            services.AddScoped<IInterviewRepository, InterviewRepository>();

            //Jobseeker
            services.AddScoped<ISignUpRequestService,SignUpRequestService>();
            services.AddScoped<ISignUpRequestRepository, SignUpRequestRepository>();
            services.AddScoped<IProfileservice, ProfileService>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IJobService,JobService>();
            services.AddScoped<IJobRepository, JobRepository>();

            services.Configure<MailSettings>(config.GetSection("MailSettings"));

            services.AddScoped<IAuthUserService, AuthUserService>();

            //Admin

            services.AddScoped<IAdminService,AdminService>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            return services;
        }
    }
}
