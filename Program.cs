using BeanSceneSystem.Data;
using BeanSceneSystem.Models;
using BeanSceneSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

//// Ensure it listens on the port provided by Render
//var port = Environment.GetEnvironmentVariable("PORT") ?? "8080"; // Default to 8080 if Render doesn't provide a port
//builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


builder.Services.AddScoped<IFileService, FileServiceOuter.FileServiceInner>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Use environment from the app context
var env = app.Environment;

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Configure the HTTP request pipeline for production
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedRolesAndAdminAsync(scope.ServiceProvider);
}


app.Run(); 