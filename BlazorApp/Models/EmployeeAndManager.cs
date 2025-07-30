namespace BlazorApp.Models;

public interface IIndividual
{
	string Name { get; set; }
}

public class Employee: IIndividual
{
	public string Name { get; set; }
}

public class Manager: IIndividual
{
	public string Name { get; set; }
}
