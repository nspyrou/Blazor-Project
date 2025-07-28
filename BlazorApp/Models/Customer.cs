using BlazorApp.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Models;

[ModelMetadataType(typeof(CustomerMetadata))]
public partial class Customer
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    public string? CompanyName { get; set; }
    public string? ContactName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
}


public class CustomerMetadata
{
	[Key]
	[DisplayName("Id")]
	[Required(ErrorMessage = "An Id has not been defined.")]
	public Guid? Id { get; set; } = Guid.NewGuid();

	[DisplayName("Company Name")]
	[MaxLength(128, ErrorMessage = "Company Name cannot exceed 128 characters long.")]
	[Required(ErrorMessage = "Company Name not set.")]
	public string? CompanyName { get; set; }
	[DisplayName("Contact Name")]
	[MaxLength(255, ErrorMessage = "Contact Name cannot exceed 255 characters long.")]
	public string? ContactName { get; set; }

	[DisplayName("Address")]
	[MaxLength(255, ErrorMessage = "Address cannot exceed 255 characters long.")]
	public string? Address { get; set; }
	[DisplayName("City")]
	[MaxLength(128, ErrorMessage = "City cannot exceed 128 characters long.")]
	public string? City { get; set; }
	[DisplayName("Region")]
	[MaxLength(128, ErrorMessage = "Region cannot exceed 128 characters long.")]
	public string? Region { get; set; }
	[DisplayName("Postal Code")]
	[MaxLength(20, ErrorMessage = "Postal Code cannot exceed 20 characters long.")]
	public string? PostalCode { get; set; }
	[DisplayName("Country")]
	[MaxLength(128, ErrorMessage = "Country cannot exceed 128 characters long.")]
	public string? Country { get; set; }
	[DisplayName("Phone Number")]
	[MaxLength(255, ErrorMessage = "Phone Number cannot exceed 255 characters long.")]
	[DataType(DataType.PhoneNumber)]
	public string? Phone { get; set; }
}

public partial class Customer : IValidatableObject
{
	private readonly AppDbContext context;

	public Customer(AppDbContext _context)
	{
		context = _context;
	}

	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		List<ValidationResult> errors = new();

		if (Id is not null)
		{
			var _customer = context.Customers.Find(Id);
			if (_customer is not null)
				errors.Add(new ValidationResult($"A customer with Id: [{_customer.Id}] already exists.", new string[] { nameof(Id) }));
		}

		return errors;
	}
}