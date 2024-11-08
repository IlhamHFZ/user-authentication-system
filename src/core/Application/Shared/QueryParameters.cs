namespace Application.Shared;

public class QueryParameters
{
	public Filtering Filtering {get; set;} = new Filtering();
	public Sorting Sorting {get; set;} = new Sorting();
	public Pagination Pagination {get; set;} = new Pagination();
}
