using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.DeleteUser;

public class DeleteUserHandler : IDeleteUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly IValidator<DeleteUserRequest> _validator;

	public DeleteUserHandler(
		IUnitofWork unitofWork, 
		IMapper mapper, 
		IValidator<DeleteUserRequest> validator)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_validator = validator;
	}

	public async Task<DeleteUserResponse?> HandleAsync(DeleteUserRequest request)
	{
		var result = _validator.Validate(request);
		if(!result.IsValid)
		{
			_validator.ValidateAndThrow(request);
		}
		
		var user = await _unitofWork.Repository<User>().GetAsync(request.Id);
		if(user is null)
		{
			return null;
		}
		
		_unitofWork.Repository<User>().Delete(user);
		
		await _unitofWork.SaveChangeAsync();
		
		return _mapper.Map<DeleteUserResponse>(user);
	}
}
