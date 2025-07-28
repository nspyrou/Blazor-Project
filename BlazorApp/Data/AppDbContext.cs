﻿using BlazorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Data;

public class AppDbContext: DbContext
{
	public DbSet<Customer> Customers { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}
}
