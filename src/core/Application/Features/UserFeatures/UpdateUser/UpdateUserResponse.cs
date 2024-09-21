using Domain.Entites;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Application.Features.UserFeatures.UpdateUser;

public record class UpdateUserResponse
{
	public string UserName {get; set;}
	public string DisplayName {get; set;}
	public Role Role {get; set;}
}
