using BlazorApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace BlazorApp.Client.Services;

public interface IRepository
{
	Task<HttpResponseMessage> NewCustomer(Customer customer);
	Task<List<Customer>> GetCustomers();
	Task<Customer?> GetCustomerById(string id);
	Task<HttpResponseMessage> UpdateCustomer(Customer customer);
	Task<HttpResponseMessage> DeleteCustomer(string id);
}

public class CustomersService : IRepository
{
	private readonly HttpClient httpClient;

	public CustomersService(HttpClient httpClient)
	{
		this.httpClient = httpClient;
	}
	public async Task<HttpResponseMessage> NewCustomer(Customer customer)
	{
		var request = await httpClient.PutAsJsonAsync("api/Customers/NewCustomer", customer);
		return request;
	}

	public async Task<HttpResponseMessage> UpdateCustomer(Customer customer)
	{
		var request = await httpClient.PostAsJsonAsync("api/Customers/UpdateCustomer", customer);
		return request;
	}

	public async Task<Customer?> GetCustomerById(string id)
	{
		var request = await httpClient.GetAsync("api/Customers/GetCustomerById/{id}");
		var response = await request.Content.ReadFromJsonAsync<Customer>();
		return response;
	}

	public async Task<List<Customer>> GetCustomers()
	{
		var request = await httpClient.GetAsync("api/Customers/GetAllCustomers");
		var response = await request.Content.ReadFromJsonAsync<List<Customer>>();
		return response;
	}

	public async Task<HttpResponseMessage> DeleteCustomer(string id)
	{
		var request = await httpClient.DeleteAsync("api/Customers/DeleteCustomer/{id}");
		return request;
	}
}
