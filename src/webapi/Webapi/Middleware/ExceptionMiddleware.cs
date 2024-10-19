using System.Net;
using FluentValidation;
using Mysqlx;
using Webapi.Models.ErrorResponse;

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
		catch(Exception)
		{
			await HandleGlobalException(context);
		}
	}
	
	private async Task HandleValidationException(HttpContext context, ValidationException ex, Stream originalBodyResponse)
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
