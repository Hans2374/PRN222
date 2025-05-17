using Microsoft.EntityFrameworkCore;
using PharmacyWebApp.Data.Models;
using PharmacyWebApp.Data.Repositories;
using PharmacyWebApp.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();

// Configure DbContext
builder.Services.AddDbContext<Lab2PharmaceuticalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IStoreAccountRepository, StoreAccountRepository>();
builder.Services.AddScoped<IMedicineInformationRepository, MedicineInformationRepository>();

// Register services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IMedicineService, MedicineService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapRazorPages();

// Redirect root to Login page
app.MapGet("/", context => {
    context.Response.Redirect("/Login");
    return Task.CompletedTask;
});

app.Run();