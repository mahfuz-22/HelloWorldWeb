using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using HelloWorldWeb.Models;

var builder = WebApplication.CreateBuilder(args);

// Key Vault configuration can be added later
// For now, using App Service Configuration

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddControllers(); // Add support for API controllers

// Add Entity Framework
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers(); // Enable API controllers

// Create database if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ContactDbContext>();
    context.Database.EnsureCreated();
}

app.Run();