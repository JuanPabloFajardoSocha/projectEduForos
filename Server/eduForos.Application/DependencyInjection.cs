using eduforos.Application.Services.UserCourseServise;
using eduForos.Application.Services.Authentication;
using eduForos.Application.Services.MeetingServices;
using eduForos.Application.Services.CourseService;
using eduForos.Application.Services.ForumServices;
using eduForos.Application.Services.UserCourseServise;
using Microsoft.Extensions.DependencyInjection;
using eduforos.Application.Services.SubjectService;
using eduforos.Application.Services.CourseSubject;
using eduforos.Application.Services.UserServices.Delete;
using eduforos.Application.Services.UserServices.Edit;
using eduforos.Application.Services.UserServices.Get;
using eduforos.Application.Services.UserServices.RecoverPassword;
using eduforos.Application.Services.UserServices.Registration;
using eduForos.Application.Common.Interfaces.Persistence;
using eduforos.Application.Services.Messages;


namespace eduForos.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<IGetUsersService, GetUsersService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IDeleteUsers, DeleteUsers>();
        services.AddScoped<IEditUserService, EditUserService>();
        services.AddScoped<IUserCourseService, UserCourseServise>();
        services.AddScoped<IRecoverPassUser, RecoverPassUser>();
        services.AddScoped<IForumService, ForumService>();
        services.AddScoped<IMeetingService, MeetingService>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<ICourseSubjectService, CourseSubjectService>();
        services.AddScoped<IMessagesService, MessagesService>();


        return services;
    }
}