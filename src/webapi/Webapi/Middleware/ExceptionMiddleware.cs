using System.Net;
using FluentValidation;

namespace Webapi.Middleware;

public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;

	public ExceptionMiddleware(RequestDelegate next)
	{
		_next = next;
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
			await HandleValidationException(context, ex, originalBodyResponse);
		}
	}
	
	private async Task HandleValidationException(HttpContext context, ValidationException ex, Stream originalBodyResponse)
	{
		using(MemoryStream memoryStream = new MemoryStream())
		{
			context.Response.Body = memoryStream;
			context.Response.ContentType = "application/json; charset=utf-8";
			context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
			
			var response = new
			{
				Status = context.Response.StatusCode,
				Message = "Validation failed",
				Errors = ex.Errors.Select(e => new {Field = e.PropertyName, Error = e.ErrorMessage})
			};
			
			context.Response.Body.Seek(0, SeekOrigin.Begin);
			await context.Response.WriteAsJsonAsync(response);
			
			context.Response.Body.Seek(0, SeekOrigin.Begin);
			await context.Response.Body.CopyToAsync(originalBodyResponse);
			context.Response.Body = originalBodyResponse;
		}
	}
	
	private async Task HandleGlobalException(HttpContext context)
	{
		context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
		
		var response = new
		{
			Status = context.Response.StatusCode,
			Message = "Internal Server Error",
		};
		
		await context.Response.WriteAsJsonAsync(response);
	}
}
