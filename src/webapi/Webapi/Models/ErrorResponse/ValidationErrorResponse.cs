namespace Webapi.Models.ErrorResponse;

public class ValidationErrorResponse
{
	public int Status {get; set;}
	public string Message {get; set;} = null!;
	public string Date {get; set;} = DateTime.Now.ToString("dddd, dd-MM-yyyy HH:mm:ss tt");
	public ICollection<ValidationErrorDetail> Errors {get; set;} = new List<ValidationErrorDetail>();
}
