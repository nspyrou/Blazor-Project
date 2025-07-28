using BlazorApp.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages;

public partial class Customers: ComponentBase
{
	private List<Customer> CustomersList { get; set; } = null!;

	protected override async Task OnInitializedAsync()
	{
		await base.OnInitializedAsync();
		CustomersList = new();
		CustomersList = await CustomerService.GetCustomers();
	}
}
