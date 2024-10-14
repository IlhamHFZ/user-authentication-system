using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using FluentValidation;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.GetByIdUser;

public class GetByIdUserHandler : IGetByIdUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	private readonly IValidator<GetByIdUserRequest> _validator;

	public GetByIdUserHandler(
		IUnitofWork unitofWork, 
		IMapper mapper, 
		IValidator<GetByIdUserRequest> validator)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
		_validator = validator;
	}

	public async Task<GetByIdUserResponse?> HandleAsync(GetByIdUserRequest request)
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
		
		return _mapper.Map<GetByIdUserResponse>(user);
	}
}
