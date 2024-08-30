using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Context;

namespace Presistence;

public static class ServiceExtensions
{
	public static void ConfigurePresistence(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<DataContext>(opt => opt.UseMySQL(configuration.GetConnectionString("Database")));
	}
}
