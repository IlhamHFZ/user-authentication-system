using System.Text.Json.Serialization;
using Application.Shared;

namespace Webapi.Models.Responses;

public record ApiResponse<T> where T: class
{
	public int Status {get; set;}
	public string Message {get; set;} = null!;
	public string Date {get; set;} = DateTime.Now.ToString("dddd, dd-MM-yyyy HH:mm:ss tt");
	public T? Data {get; set;}
	
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Filtering? Filtering {get; set;}
	
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Sorting? Sorting {get; set;}
	
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public Pagination? Pagination {get; set;}
}
