namespace Webapi.Models;

public class ApiResponse<T> where T: class?
{
	public int Status {get; set;}
	public string Message {get; set;} = null!;
	public string Date {get; set;} = DateTime.Now.ToString("dddd, dd-MM-yyyy HH:mm:ss tt");
	public T? Data {get; set;} 
	
}
