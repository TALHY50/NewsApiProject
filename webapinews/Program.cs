using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using webapinews.Authorization;
using webapinews.ExceptionHandler;
using webapinews.Interface;
using webapinews.Models;
using webapinews.Reporistory;
using webapinews.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Builder;
using webapinews.Entities;
using Microsoft.Extensions.DependencyInjection.Extensions;
//using webapinews.Models;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    var env = builder.Environment;
    // Add services to the container.
    builder.Services.AddDbContext<NewsApiContext>(opt =>
    opt.UseSqlServer("NewAPI"));
    builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddCors();
//builder.Services.AddControllers().AddJsonOptions(x =>
//{
//        // serialize enums as strings in api responses (e.g. Role)
//    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//});

    // configure strongly typed settings object
    builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    builder.Services.AddScoped<IJwtAuth, JwtAuth>();
    builder.Services.AddScoped<IUserReporistory, UserReporistory>();
    builder.Services.AddScoped<INewsReporistory, NewsReporistory>();
    builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddScoped(typeof(IIdentityService), typeof(IdentityServices));


    builder.Services.AddScoped<IBookMarkReporistory, BookMarkReporistory>();

}
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "NEWS API", Version = "PRO" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
var app = builder.Build();
// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
    app.UseSwagger();
    app.UseSwaggerUI();
    }
app.UseHttpsRedirection();
// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();
// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();
app.Run();
