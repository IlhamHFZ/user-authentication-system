namespace Webapi.Models.Errors.ValidationException;

public class ValidationErrorDetail
{
	public string Field {get; set;} = null!;
	public ICollection<string> Message {get; set;} = new List<string>();
}
