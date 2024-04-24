using Microsoft.AspNetCore.Authentication.Cookies;

using PryEcommerce.Infraestructura;
using PryEcommerce.Negocios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string? connectionString = builder.Configuration.GetConnectionString("cn1");

if (connectionString != null)
{
    builder.Services.AddScoped<LoginServicio>(login => new LoginServicio(new LoginRepository(connectionString)));
}

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.AccessDeniedPath = "";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

app.Run();