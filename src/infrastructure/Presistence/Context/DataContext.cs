using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;

namespace Presistence.Context;

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
		
		IList<Role> roles = new List<Role>()
		{
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
		};
		
		IList<User> users = new List<User>()
		{
			new User()
			{
				Id = Guid.Parse("0afb2aa1-d314-485c-8552-e29a546b1321"),
				UserName = "Hikaru Utada",
				DisplayName = "Kuma Power"
			},
			new User()
			{
				Id = Guid.Parse("253cd4c1-4b3f-4a2a-83ae-269c3bdb7879"),
				UserName = "Hatsune Miku",
				DisplayName = "Miku 39"
			},
			new User()
			{
				Id = Guid.Parse("656b6830-6908-4d5a-81a6-a20d98bf7d2d"),
				UserName = "Megurin Luka",
				DisplayName = "Luka"
			}
		};
		
		IList<IdentityUserRole<Guid>> userroles = new List<IdentityUserRole<Guid>>()
		{
			new IdentityUserRole<Guid>()
			{
				UserId = users[0].Id,
				RoleId = roles[0].Id
			},
			new IdentityUserRole<Guid>()
			{
				UserId = users[1].Id,
				RoleId = roles[0].Id
			},
			new IdentityUserRole<Guid>()
			{
				UserId = users[2].Id,
				RoleId = roles[0].Id
			}
		};
				
		modelBuilder.Entity<Role>().HasData(roles);
		modelBuilder.Entity<User>().HasData(users);
		modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(userroles);
	}
}
