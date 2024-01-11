using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Application.Authentication.Interface;
using Application.Authentication.Services;
using Application.Data.Data;
using Application.Core.Helpers;
using Application.Core.Helpers.Interfaces;
using Application.Core.Interfaces;
using Application.Core.Mappers;
using Application.Data.Models.Core;
using Application.Core.Repositories;
using Application.Core.Repositories.Interfaces;
using Application.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IValidationService<>), typeof(ValidationService<>));

builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IBusinessAreaFilteringService,BusinessAreaFilteringService>();
builder.Services.AddScoped<IBusinessAreaTypeService,BusinessAreaTypeService>();
builder.Services.AddScoped<IBusinessAreaService,BusinessAreaService>();
builder.Services.AddScoped<IAuthService, AuthService>();

#region ...Registering Auto Mapper
var automapper = new MapperConfiguration(item => item.AddProfile(new AutoMappers()));
IMapper mapper = automapper.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region ...Registering Services
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("InductionDb"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2",new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Authentication:Secret_key").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options => options.AddPolicy("ClientApp", policy =>
{
    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));
#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("ClientApp");

app.MapControllers();

app.Run();
