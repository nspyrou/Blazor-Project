using BlazorApp.Models;

namespace BlazorApp.Objects;

public class IndividualExtractor
{
	public string GetIndividualName(IIndividual individual)
	{
		return individual.Name;
	}
}
