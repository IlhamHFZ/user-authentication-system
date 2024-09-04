using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Context;

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
