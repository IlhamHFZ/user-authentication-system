using System.Linq.Expressions;
using Application.Features.UserFeatures.Interface;
using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Features.UserFeatures.GetAllUser;

public class GetAllUserHandler : IGetAllUserHandler
{
	private readonly UserManager<User> _userManager;
	private readonly IMapper _mapper;
	private readonly ILogger<GetAllUserHandler> _logger;

	public GetAllUserHandler(
		UserManager<User> userManager,
		IMapper mapper, 
		ILogger<GetAllUserHandler> logger)
	{
		_userManager = userManager;
		_mapper = mapper;
		_logger = logger;
	}

	public  Task<IEnumerable<GetAllUserResponse>> HandleAsync(GetAllUserRequest request)
	{
		_logger.LogInformation($"Starting to process GetAllUserHandler request");
		if(request.QueryParameters.Pagination.PageSize <= 0 || request.QueryParameters.Pagination.PageCurrent <= 0)
		{
			_logger.LogWarning($"Page size: {request.QueryParameters.Pagination.PageSize}, Page Current: {request.QueryParameters.Pagination.PageCurrent}, can not take negative number");
			return Task.FromResult(Enumerable.Empty<GetAllUserResponse>());
		}
		
		if(string.IsNullOrWhiteSpace(request.QueryParameters.Filtering.FilterQuery))
		{
			_logger.LogInformation("Filtering is null or empty, defaulting to empty string");
			request.QueryParameters.Filtering.FilterQuery = string.Empty;
		}
		
		_logger.LogInformation($"Filter query: {request.QueryParameters.Filtering.FilterQuery}, Filter on: {request.QueryParameters.Filtering.FilterOn}, Sort by: {request.QueryParameters.Sorting.SortBy}, Ascending: {request.QueryParameters.Sorting.IsAscending}, Page: {request.QueryParameters.Pagination.PageCurrent}, Page size: {request.QueryParameters.Pagination.PageSize}");
		Expression<Func<User, bool>> expressionFilter = request.QueryParameters.Filtering.FilterOn.ToLower() switch 
		{
			"username" => u => u.NormalizedUserName.Contains(request.QueryParameters.Filtering.FilterQuery),
			"email" => u => u.NormalizedEmail.Contains(request.QueryParameters.Filtering.FilterQuery),
			"displayname" => u => u.NormalizeDisplayName.Contains(request.QueryParameters.Filtering.FilterQuery),
			_ => u => u.NormalizeDisplayName.Contains(request.QueryParameters.Filtering.FilterQuery)
		};
		_logger.LogInformation($"Applied filter expression base in filter criteria");
		
		Expression<Func<User, object>> expressionSort = request.QueryParameters.Sorting.SortBy.ToLower() switch
		{
			"emailconfirmed" => u => u.EmailConfirmed,
			"phonenumberconfirmed" => u => u.PhoneNumberConfirmed,
			"twofactorenabled" => u => u.TwoFactorEnabled,
			"accessfailedcount" => u => u.AccessFailedCount,
			"username" => u => u.UserName,
			"email" => u => u.Email,
			"displayname" => u => u.DisplayName,
			_ => u => u.UserName
		};
		_logger.LogInformation("Applied sorting expression based on the sort criteria");
		
		var skipData = (request.QueryParameters.Pagination.PageCurrent - 1) * request.QueryParameters.Pagination.PageSize;
		_logger.LogInformation($"Calculated skip data: {skipData}");
		
		var users = _userManager.Users
			.AsNoTracking()
			.Where(expressionFilter)
			.Skip(skipData)
			.Take(request.QueryParameters.Pagination.PageSize);
		_logger.LogInformation($"Filtered and pagination users. Now applying sorting");
		
		users = request.QueryParameters.Sorting.IsAscending ? users.OrderBy(expressionSort) : users.OrderByDescending(expressionSort);
		_logger.LogInformation($"Sorting applied: {(request.QueryParameters.Sorting.IsAscending ? "Ascending" : "Descending")}");
		
		if(users is null)
		{
			_logger.LogWarning("No users found for the given criteria");
			return Task.FromResult(Enumerable.Empty<GetAllUserResponse>());
		}
		
		_logger.LogInformation($"Successfully retrieved {users.Count()} users for the given criteria");
		return Task.FromResult(_mapper.Map<IEnumerable<GetAllUserResponse>>(users));
	}
}
