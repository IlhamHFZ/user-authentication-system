using System.Text;
using Application.Repository.Interface;
using Domain.Entites;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presistence.Context;
using Presistence.Repository;
using Presistence.Repository.Interface;
using System.Reflection;

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

builder.Services.AddIdentity<User, Role>(opt =>
{
	opt.Password.RequireDigit = true;
	opt.Password.RequireLowercase = true;
	opt.Password.RequireUppercase = true;
	opt.Password.RequiredUniqueChars = 1;
	opt.User.RequireUniqueEmail = true;
	
})	
	.AddEntityFrameworkStores<DataContext>()
	.AddDefaultTokenProviders();

builder.Services.AddScoped<IUnitofWork, UnitofWork>();
builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<IRepository<Role>, Repository<Role>>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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

app.MapControllers();

app.Run();
