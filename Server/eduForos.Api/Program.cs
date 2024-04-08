using eduForos.Api.Errors;
using eduForos.Application;
using eduForos.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, EduForosProblemDetailsFactory>();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("myPolicy", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
                 
        });
    });
}


var app = builder.Build();
{
    app.UseCors("myPolicy");
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();

}


