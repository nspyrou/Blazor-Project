using BlazorApp.Data;
using BlazorApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
	private readonly AppDbContext appDbContext;

	public CustomersController(AppDbContext AppDbContext)
	{
		appDbContext = AppDbContext;
	}

	[HttpGet("GetAllCustomers")]
	public async Task<ActionResult<List<Customer>>> GetAllCustomersAsync()
	{
		var customers = await appDbContext.Customers.AsNoTracking().ToListAsync();
		return Ok(customers);
	}

	[HttpGet("GetCustomerById/{id}")]
	public async Task<ActionResult<Customer>> GetCustomerById(string id)
	{
		var customer = await appDbContext.Customers.FirstOrDefaultAsync(x => (x.Id.ToString() == id));
		return Ok(customer);
	}

	[HttpPost("NewCustomer")]
	public async Task<ActionResult> InsertCustomer(Customer customer)
	{
		if (ModelState.IsValid)
		{
			List<ValidationResult> errors = new();
			if (Validator.TryValidateObject(customer, new ValidationContext(customer), errors, true))
			{
				appDbContext.Customers.Add(customer);
				if (await appDbContext.SaveChangesAsync() > 0)
					return Ok(customer);
				else
					return BadRequest("Failed to save new information to database, please try again.");
			}
			else
				return BadRequest(errors);
		}
		else
			return BadRequest(ModelStateErrors(ModelState));
	}

	[HttpDelete("DeleteCustomer")]
	public async Task<ActionResult> DeleteCustomer(string id)
	{
		var customer = await appDbContext.Customers.FindAsync(id);
		if (customer is not null)
		{
			appDbContext.Customers.Remove(customer);
			if (await appDbContext.SaveChangesAsync() > 0)
				return Ok();
			else 
				return BadRequest();
		}

		return BadRequest("Requested Customer not found.");
	}

	[HttpPut("UpdateCustomer")]
	public async Task<ActionResult> UpdateCustomer(Customer customer)
	{
		if (ModelState.IsValid)
		{
			List<ValidationResult> errors = new();
			if (Validator.TryValidateObject(customer, new ValidationContext(customer), errors, true))
			{
				appDbContext.Update(customer);
				if (await appDbContext.SaveChangesAsync() > 0)
					return Ok(customer);
				else
					return BadRequest("Failed to update database. Please try again.");
			}
			else
				return BadRequest(errors);
		}

		return BadRequest(ModelStateErrors(ModelState));
	}

	private List<ValidationResult> ModelStateErrors(ModelStateDictionary modelState)
	{
		var modelErrors = modelState.Values.SelectMany(v => v.Errors)
								.Select(v => v.ErrorMessage + " " + v.Exception).ToList();

		List<ValidationResult> verrors = new();
		foreach (var error in modelErrors)
			verrors.Add(new ValidationResult(error));

		return verrors;
	}
}
