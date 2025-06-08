using GameStore.Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Set culture to ensure proper decimal handling
var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add DbContext for database access
builder.Services.AddDbContext<GameStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity for authentication and authorization
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Configure Identity options if needed
})
    .AddEntityFrameworkStores<GameStoreDbContext>();

// Configure cookie-based authentication with Identity
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
    options.LogoutPath = "/Logout";
});

// Register Business Layer Services
builder.Services.AddScoped<GameStore.Business.Interfaces.IGameService, GameStore.Business.Services.GameService>();
builder.Services.AddScoped<GameStore.Business.Interfaces.ICategoryService, GameStore.Business.Services.CategoryService>();

// Register Data Layer Repositories
builder.Services.AddScoped<GameStore.Data.Repositories.IGameRepository, GameStore.Data.Repositories.GameRepository>();
builder.Services.AddScoped<GameStore.Data.Repositories.ICategoryRepository, GameStore.Data.Repositories.CategoryRepository>();

// Add Razor Pages
builder.Services.AddRazorPages();

// Configure request localization
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en-US" };
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Add localization middleware
app.UseRequestLocalization();

app.UseRouting();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<GameStoreDbContext>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    // Ensure database is created
    context.Database.EnsureCreated();

    // Seed Roles
    string[] roles = { "Admin", "Manager", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Seed Users
    // Admin user
    var adminUser = new IdentityUser
    {
        UserName = "admin@example.com",
        Email = "admin@example.com",
        EmailConfirmed = true
    };
    if (await userManager.FindByEmailAsync("admin@example.com") == null)
    {
        await userManager.CreateAsync(adminUser, "Admin@123");
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

    // Manager user
    var managerUser = new IdentityUser
    {
        UserName = "manager@example.com",
        Email = "manager@example.com",
        EmailConfirmed = true
    };
    if (await userManager.FindByEmailAsync("manager@example.com") == null)
    {
        await userManager.CreateAsync(managerUser, "Manager@123");
        await userManager.AddToRoleAsync(managerUser, "Manager");
    }

    // Regular user
    var regularUser = new IdentityUser
    {
        UserName = "user@example.com",
        Email = "user@example.com",
        EmailConfirmed = true
    };
    if (await userManager.FindByEmailAsync("user@example.com") == null)
    {
        await userManager.CreateAsync(regularUser, "User@123");
        await userManager.AddToRoleAsync(regularUser, "User");
    }

    // Seed Categories
    if (!context.Categories.Any())
    {
        var categories = new[]
        {
            new Category { Name = "Action" },
            new Category { Name = "Adventure" },
            new Category { Name = "RPG" },
            new Category { Name = "Sports" },
            new Category { Name = "Strategy" },
            new Category { Name = "Simulation" },
            new Category { Name = "Puzzle" },
            new Category { Name = "Racing" }
        };
        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();
    }

    // Seed Games
    if (!context.Games.Any())
    {
        // Get categories for foreign keys
        var actionCategory = context.Categories.First(c => c.Name == "Action");
        var adventureCategory = context.Categories.First(c => c.Name == "Adventure");
        var rpgCategory = context.Categories.First(c => c.Name == "RPG");
        var sportsCategory = context.Categories.First(c => c.Name == "Sports");
        var strategyCategory = context.Categories.First(c => c.Name == "Strategy");

        var games = new[]
        {
            new Game
            {
                Title = "The Last Guardian",
                Price = 59.99m,
                ReleaseDate = new DateTime(2024, 3, 15),
                CategoryId = adventureCategory.Id
            },
            new Game
            {
                Title = "Cyber Strike 2077",
                Price = 69.99m,
                ReleaseDate = new DateTime(2023, 11, 10),
                CategoryId = actionCategory.Id
            },
            new Game
            {
                Title = "Dragon's Legacy",
                Price = 49.99m,
                ReleaseDate = new DateTime(2024, 1, 20),
                CategoryId = rpgCategory.Id
            },
            new Game
            {
                Title = "FIFA 2024",
                Price = 59.99m,
                ReleaseDate = new DateTime(2023, 9, 29),
                CategoryId = sportsCategory.Id
            },
            new Game
            {
                Title = "Civilization VII",
                Price = 79.99m,
                ReleaseDate = new DateTime(2024, 2, 11),
                CategoryId = strategyCategory.Id
            },
            new Game
            {
                Title = "Space Odyssey",
                Price = 39.99m,
                ReleaseDate = new DateTime(2023, 7, 4),
                CategoryId = adventureCategory.Id
            },
            new Game
            {
                Title = "Battlefield 2042",
                Price = 69.99m,
                ReleaseDate = new DateTime(2023, 10, 22),
                CategoryId = actionCategory.Id
            },
            new Game
            {
                Title = "The Witcher 4",
                Price = 69.99m,
                ReleaseDate = new DateTime(2024, 5, 17),
                CategoryId = rpgCategory.Id
            },
            new Game
            {
                Title = "NBA 2K24",
                Price = 59.99m,
                ReleaseDate = new DateTime(2023, 9, 8),
                CategoryId = sportsCategory.Id
            },
            new Game
            {
                Title = "Age of Empires V",
                Price = 49.99m,
                ReleaseDate = new DateTime(2024, 4, 28),
                CategoryId = strategyCategory.Id
            }
        };
        context.Games.AddRange(games);
        await context.SaveChangesAsync();
    }
}

app.MapRazorPages();

app.Run();