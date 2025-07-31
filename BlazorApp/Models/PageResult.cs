namespace BlazorApp.Models;

public class PageResult<T> where T: class 
{
	public List<T> Entries { get; set; }
	public int TotalEntries { get; set; }
}
