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
using Application.DataTransfer.Dtos.Core;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


#region ...Registering Auto Mapper
var automapper = new MapperConfiguration(item => item.AddProfile(new AutoMappers()));
IMapper mapper = automapper.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region ...Registering Services
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<IDirectoryService<Applications, ApplicationsDto>, DirectoryService<Applications, ApplicationsDto>>();
builder.Services.AddScoped<IDirectoryService<Customer, CustomersDto>, DirectoryService<Customer, CustomersDto>>();
builder.Services.AddScoped<IDirectoryService<BusinessArea, BusinessAreaDto>, DirectoryService<BusinessArea, BusinessAreaDto>>();
builder.Services.AddScoped<IDirectoryService<BusinessAreaType, BusinessAreaTypeDto>, DirectoryService<BusinessAreaType, BusinessAreaTypeDto>>();
builder.Services.AddScoped<IDirectoryService<Person, PersonDto>, DirectoryService<Person, PersonDto>>();
builder.Services.AddScoped<IDirectoryService<ApplicationCustomer, ApplicationCustomerDto>, DirectoryService<ApplicationCustomer, ApplicationCustomerDto>>();

builder.Services.AddScoped(typeof(IValidationHelper<>), typeof(ValidationHelper<>));
builder.Services.AddScoped<INewInstanceHelper, NewInstanceHelper>();
builder.Services.AddScoped<IBusinessAreaFilteringService, BusinessAreaFilteringService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IApplicationCustomerService, ApplicationCustomerService>();

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
