using System.Text;
using Domain.Entites;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presistence.Context;
using Presistence.Repository;
using Presistence.Repository.Interface;
using System.Reflection;
using FluentValidation;
using Application.Features.UserFeatures;
using Application.Features.UserFeatures.Interface;
using Application.Features.UserFeatures.GetAllUser;
using Application.Features.UserFeatures.CreateUser;
using Application.Features.UserFeatures.DeleteUser;
using Application.Features.UserFeatures.GetByIdUser;
using Application.Features.UserFeatures.UpdateUser;
using Application.Features.UserFeatures.UpdateUserProfile;
using Webapi.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt =>
{
	opt.UseMySQL(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			RequireExpirationTime = true,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
			ValidAudience = builder.Configuration["Jwt:Audience"],
			ValidIssuer = builder.Configuration["Jwt:Issuer"]
		};
	});

builder.Services.AddIdentity<User, Role>(options =>
{
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;
	options.Password.RequiredUniqueChars = 1;
	options.User.RequireUniqueEmail = true;
})
	.AddEntityFrameworkStores<DataContext>()
	.AddDefaultTokenProviders();

builder.Services.AddLogging(logging => 
{
	logging.ClearProviders();
});
builder.Services.AddSerilog(config =>
{
	config.ReadFrom.Configuration(builder.Configuration);
});

builder.Services.AddScoped<IUnitofWork, UnitofWork>();

builder.Services.AddScoped<IUserFacade, UserFacade>();

builder.Services.AddScoped<ICreateUserHandler, CreateUserHandler>();
builder.Services.AddScoped<IDeleteUserHandler, DeleteUserHandler>();
builder.Services.AddScoped<IGetAllUserHandler, GetAllUserHandler>();
builder.Services.AddScoped<IGetByIdUserHandler, GetByIdUserHandler>();
builder.Services.AddScoped<IUpdateUserHandler, UpdateUserHandler>();
builder.Services.AddScoped<IUpdateUserProfileHandler, UpdateUserProfileHandler>();

builder.Services.AddAutoMapper(Assembly.Load("Application"));
builder.Services.AddValidatorsFromAssembly(Assembly.Load("Application"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(options =>
{
	options.GetLevel = (context, elapsed, ex) => 
	{
		if(context.Response.StatusCode >= 400)
		{
			return Serilog.Events.LogEventLevel.Warning;
		}
		
		if(context.Response.StatusCode >= 500)
		{
			return Serilog.Events.LogEventLevel.Error;
		}
		
		return Serilog.Events.LogEventLevel.Information;
	};
});
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
