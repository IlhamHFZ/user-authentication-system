using Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;

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
		
		modelBuilder.Entity<Role>().HasData(
			new Role()
			{
				Id = Guid.Parse("a2b46be9-b47b-45eb-9083-bc0d2387c462"), 
				ConcurrencyStamp = "a2b46be9-b47b-45eb-9083-bc0d2387c462", 
				Name = "Internal", 
				NormalizedName = "internal".ToUpper()
			},
			new Role()
			{
				Id = Guid.Parse("90f07141-cc62-4d80-b210-dd5632cdb27d"),
				ConcurrencyStamp = "90f07141-cc62-4d80-b210-dd5632cdb27d",
				Name = "Admin",
				NormalizedName = "admin".ToUpper()
			},
			new Role()
			{
				Id = Guid.Parse("9ac230f2-3502-447a-92eb-461f9adc475d"),
				ConcurrencyStamp = "9ac230f2-3502-447a-92eb-461f9adc475d",
				Name = "Goole",
				NormalizedName = "google".ToUpper()
			}
		);
	}
}
