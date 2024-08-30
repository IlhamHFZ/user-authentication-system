using Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Context;
/*
	TODO:
	- ganti Class1 jadi nama class untuk database
	- install nuget yang diperluin untuk code fisrt EF
	- pake fluent api
*/
public class DataContext : IdentityDbContext<User, Role, Guid>
{
	public DataContext(DbContextOptions<DataContext> options): base(options)
	{
		
	}
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		
		modelBuilder.Entity<User>(user =>
		{
			user.Property(u => u.DisplayName).IsRequired();
		});
	}
}
