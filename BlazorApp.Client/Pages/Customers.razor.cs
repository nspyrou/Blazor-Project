using BlazorApp.Client.Services;
using BlazorApp.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages;

public partial class Customers: ComponentBase
{
	private List<Customer> CustomersList { get; set; } = new();
	private int _page = 1;
	private int _pageSize = 10;
	private int _totalCount;

	protected void AddNewCustomer() => NavigationService.NavigateTo("managecustomer");

	protected override async Task OnInitializedAsync()
	{
		CustomersList = new();
		await LoadPagedCustomers(); 
	}

	private async Task LoadPagedCustomers()
	{
		var result = await CustomersService.GetPagedCustomers(_page, _pageSize); 
		CustomersList = result.Entries;
		_totalCount = result.TotalEntries;
	}

	private async Task NextPage()
	{
		_page++;
		await LoadPagedCustomers();
	}

	private async Task PreviousPage()
	{
		if (_page > 1)
		{
			_page--;
			await LoadPagedCustomers();
		}
	}

	protected override Task OnAfterRenderAsync(bool firstRender)
	{
		return base.OnAfterRenderAsync(firstRender);
	}

	public async Task PromptDelete(Guid? Id)
	{
		var selCustomer = CustomersList.FirstOrDefault(x => x.Id == Id);
		if (selCustomer != null)
		{
			bool confirmed = await JS.InvokeAsync<bool>("confirm", $"Are you sure you want to remove [{selCustomer.CompanyName}] entry?");
			if (confirmed)
			{
				var customer = CustomersList.FirstOrDefault(c => c.Id == Id);
				if (customer != null)
				{
					var resp = await CustomersService.DeleteCustomer(Id.ToString());
					if (resp.IsSuccessStatusCode)
						CustomersList = await CustomersService.GetCustomers();
				}
			}
		}
	}

	public void HandleEdit(Guid? Id)
	{
		NavigationService.NavigateTo($"managecustomer/{Id}");
	}
	
	
}
