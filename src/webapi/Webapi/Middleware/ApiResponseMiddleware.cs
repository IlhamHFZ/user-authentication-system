using System.Text;
using System.Text.Json;

namespace Webapi.Middleware;

public class ApiResponseMiddleware
{
	private readonly RequestDelegate _next;

	public ApiResponseMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		Stream originalBodyResponse = context.Response.Body;

		using (MemoryStream newBodyResponse = new MemoryStream())
		{
			context.Response.Body = newBodyResponse;

			await _next(context);

			if(IsJsonResponse(context.Response))
			{
				await WriteJsonApiResponse(context, originalBodyResponse);
			}
			else
			{
				newBodyResponse.Seek(0, SeekOrigin.Begin);
				await newBodyResponse.CopyToAsync(originalBodyResponse);
				context.Response.Body = originalBodyResponse;
			}

		}
	}

	private async Task WriteJsonApiResponse(HttpContext context, Stream originalBodyResponse)
	{
		context.Response.Body.Seek(0, SeekOrigin.Begin);
		StringBuilder stringNewBody = new StringBuilder(await new StreamReader(context.Response.Body).ReadToEndAsync());
		StringBuilder message = new StringBuilder();
		
		if(context.Response.StatusCode == 200)
		{
			message.Append("success");
		}
		
		if(context.Response.StatusCode == 404)
		{
			message.Append(stringNewBody.ToString());
			message.Replace("\"", "");
			stringNewBody.Clear();
		}
			
		var response = new
		{
			Status = context.Response.StatusCode,
			Message = message.ToString(),
			Date = DateTime.Now.ToString("dddd, dd-MM-yyyy HH:mm:ss tt"),
			Data = stringNewBody.Length != 0 ? JsonSerializer.Deserialize<object>(stringNewBody.ToString()) : null
		};
		
		context.Response.Body.Seek(0, SeekOrigin.Begin);
		await context.Response.WriteAsJsonAsync(response);
		
		context.Response.Body.Seek(0, SeekOrigin.Begin);
		await context.Response.Body.CopyToAsync(originalBodyResponse);
		
		context.Response.Body = originalBodyResponse;
	}

	private bool IsJsonResponse(HttpResponse httpResponse)
	{
		return httpResponse.ContentType != null && httpResponse.ContentType.Contains("application/json");
	}
}
