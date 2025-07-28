using BlazorApp.Client.Services;
using BlazorApp.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages;

public partial class Customers: ComponentBase
{

	private List<Customer> CustomersList { get; set; } = null!;

	//ConfirmationModal confirmationModal { get; set; }

	protected override async Task OnInitializedAsync()
	{
		CustomersList = new();
		CustomersList = await CustomersService.GetCustomers();
	}

	protected override Task OnAfterRenderAsync(bool firstRender)
	{
		return base.OnAfterRenderAsync(firstRender);
	}
}
