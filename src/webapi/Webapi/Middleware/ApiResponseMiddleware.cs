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
		
		using(MemoryStream newBodyResponse = new MemoryStream())
		{
			context.Response.Body = newBodyResponse;
			
			await _next(context);
			
			newBodyResponse.Seek(0, SeekOrigin.Begin);
			string stringNewBody = await new StreamReader(newBodyResponse).ReadToEndAsync();
			
			newBodyResponse.Seek(0, SeekOrigin.Begin);
			JsonWriterOptions jsonWriterOptions = new JsonWriterOptions()
			{
				Indented = true
			};
			using(Utf8JsonWriter jsonWriter = new Utf8JsonWriter(newBodyResponse))
			{
				jsonWriter.WriteStartObject();
				jsonWriter.WriteNumber("status", context.Response.StatusCode);
				jsonWriter.WriteString("date", DateTime.Now.ToString("dddd, dd MM yyyy HH:mm:ss tt"));
				jsonWriter.WritePropertyName("data");
				jsonWriter.WriteRawValue(stringNewBody);
				await jsonWriter.FlushAsync();
				jsonWriter.WriteEndObject();
			}
			
			newBodyResponse.Seek(0, SeekOrigin.Begin);
			await newBodyResponse.CopyToAsync(originalBodyResponse);
			context.Response.Body = originalBodyResponse;
		}
	}
}
