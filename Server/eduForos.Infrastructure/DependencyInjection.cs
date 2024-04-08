using eduforos.Application.Common.Interfaces.Persistence;
using eduforos.Infrastructure.Persistence;
using eduforos.Infrastructure.Settings.Others;
using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Application.Common.Interfaces.Services.Cloudinary;
using eduForos.Application.Common.Interfaces.Services.Others;
using eduForos.Infrastructure.Context;
using eduForos.Infrastructure.Persistence;
using eduForos.Infrastructure.Services.Authentication;
using eduForos.Infrastructure.Services.CloudinaryServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eduForos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure
    (this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<Settings.Authentication.JwtSettings>(configuration.GetSection(Settings.Authentication.JwtSettings.SectionName));
        services.AddSingleton<Application.Common.Interfaces.Services.Authentication.IJwtTokenService, JwtTokenService>();
       // services.AddScoped<IJitsiService, Jitsi>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<IEmailService, Email>();

        services.AddScoped<IMeetingRepository, MeetingRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IUserCourseRepository, UserCourseRepository>();
        
        services.AddScoped<IForumRepository, ForumRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<ICourseSubjectRepository, CourseSubjectRepository>();

        services.AddDbContext<EduForosDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConPablo")));

        return services;
    }
}