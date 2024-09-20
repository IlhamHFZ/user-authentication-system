using Application.Repository.Interface;
using AutoMapper;
using Domain.Entites;
using Presistence.Repository.Interface;

namespace Application.Features.UserFeatures.CreateUser;

public class CreateUserHandler
{
	private readonly IUnitofWork _unitofWork;
	private readonly IMapper _mapper;
	
	public CreateUserHandler(IUnitofWork unitofWork, IMapper mapper)
	{
		_unitofWork = unitofWork;
		_mapper = mapper;
	}
	
	public async Task Handle(CreateUserRequest request)
	{
		
		var validator = new CreateUserValidator();
		var result = validator.Validate(request);
		/*TODO:
			-implement best practice expection handler to give a feedback to user
			-implement loger
		*/
		if(!result.IsValid)
		{
			return;
		}
		var user = _mapper.Map<User>(request);
		await _unitofWork.Repository<User>().CreateAsync(user);
		await _unitofWork.SaveChangeAsync();
	}
}
