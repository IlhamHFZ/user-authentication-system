using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.CreateUser;

public class CreateUserHandler : ICreateUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly IValidator<CreateUserRequest> _validator;

	public CreateUserHandler(
		IUnitofWork unitofWork, 
		IMapper mapper, 
		IValidator<CreateUserRequest> validator)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_validator = validator;
	}

	public async Task<CreateUserResponse> HandleAsync(CreateUserRequest request)
	{
		var result = _validator.Validate(request);
		if(!result.IsValid)
		{
			_validator.ValidateAndThrow(request);
		}
		
		var user = _mapper.Map<User>(request);
		await _unitofWork.Repository<User>().CreateAsync(user);
		
		await _unitofWork.SaveChangeAsync();
		
		return _mapper.Map<CreateUserResponse>(user);
	}
}
