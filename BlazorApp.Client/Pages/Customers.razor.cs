using BlazorApp.Client.Pages.Modals;
using BlazorApp.Client.Services;
using BlazorApp.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages;

public partial class Customers: ComponentBase
{

	private List<Customer> CustomersList { get; set; } = null!;

	ConfirmationModal confirmationModal { get; set; }

	protected void AddNewCustomer() => NavigationService.NavigateTo("managecustomer");

	protected override async Task OnInitializedAsync()
	{
		CustomersList = new();
		CustomersList = await CustomersService.GetCustomers();
	}

	protected override Task OnAfterRenderAsync(bool firstRender)
	{
		return base.OnAfterRenderAsync(firstRender);
	}

	public void HandleDelete(Guid? Id)
	{
		var selCustomer = CustomersList.FirstOrDefault(x => x.Id == Id);
		if (selCustomer != null)
		{
			confirmationModal.DialogMessage = $"Are you sure you want to remove [{selCustomer.CompanyName}] entry?";
			confirmationModal.ShowButtons = (int)ModalButtonsEnum.mbYes | (int)ModalButtonsEnum.mbNo;
			confirmationModal?.ShowDialog();
		}
	}

	public void HandleEdit(Guid? Id)
	{
		NavigationService.NavigateTo($"managecustomer/{Id}");
	}
	
	
}
