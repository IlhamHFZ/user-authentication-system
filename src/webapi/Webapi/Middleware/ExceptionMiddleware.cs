using System.Net;
using FluentValidation;
using Webapi.Models.ErrorResponse;

namespace Webapi.Middleware;

public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionMiddleware> _logger;

	public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		Stream originalBodyResponse = context.Response.Body;
	
		try
		{
			await _next(context);
		}
		catch(ValidationException ex)
		{
			await HandleValidationException(context, ex, originalBodyResponse, _logger);
		}
		catch(Exception ex)
		{
			await HandleGlobalException(context, ex, _logger);
		}
	}
	
	private async Task HandleValidationException(HttpContext context, ValidationException ex, Stream originalBodyResponse, ILogger<ExceptionMiddleware> logger)
	{
		using(MemoryStream memoryStream = new MemoryStream())
		{
			context.Response.Body = memoryStream;
			context.Response.ContentType = "application/json; charset=utf-8";
			context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
			
			var response = new ValidationErrorResponse()
			{
				Status = context.Response.StatusCode,
				Message = "validation failed",
				Errors = ex.Errors.GroupBy(e => e.PropertyName).Select(e => new ValidationErrorDetail()
				{
					Field = e.Key,
					Message = e.Select(m => m.ErrorMessage).ToList()
				}).ToList()
			};
			
			logger.LogWarning(ex, $"Validation failed for {context.Request.Method} {context.Request.Path}");
			
			context.Response.Body.Seek(0, SeekOrigin.Begin);
			await context.Response.WriteAsJsonAsync(response);
			
			context.Response.Body.Seek(0, SeekOrigin.Begin);
			await context.Response.Body.CopyToAsync(originalBodyResponse);
			context.Response.Body = originalBodyResponse;
		}
	}
	
	private async Task HandleGlobalException(HttpContext context, Exception ex, ILogger<ExceptionMiddleware> logger)
	{
		logger.LogError(ex, $"An unexpected error occurred processing request {context.Request.Method} {context.Request.Path}");
		
		context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
		
		var response = new
		{
			Status = context.Response.StatusCode,
			Message = "Internal Server Error",
		};
		
		await context.Response.WriteAsJsonAsync(response);
	}
}
