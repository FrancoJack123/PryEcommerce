using Microsoft.AspNetCore.Authentication.Cookies;

using PryEcommerce.Infraestructura;
using PryEcommerce.Negocios;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

string? connectionString = builder.Configuration.GetConnectionString("cn1");

if (connectionString != null)
{
    builder.Services.AddScoped<LoginServicio>(login => new LoginServicio(new LoginRepository(connectionString)));
    builder.Services.AddScoped<CategoriaServicio>(c => new CategoriaServicio(new CategoriaRepository(connectionString)));
    builder.Services.AddScoped<MarcaServicio>(m => new MarcaServicio(new MarcaRepository(connectionString)));
    builder.Services.AddScoped<UsuarioServicio>(u => new UsuarioServicio(new UsuarioRepository(connectionString)));
    builder.Services.AddScoped<ProductoServicio>(p => new ProductoServicio(new ProductoRepository(connectionString)));
    builder.Services.AddScoped<VentaServicio>(p => new VentaServicio(new VentaRepository(connectionString)));
    builder.Services.AddScoped<DashboardServicio>(p => new DashboardServicio(new DashboardRepository(connectionString)));
}

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.AccessDeniedPath = "/Shop/Index";
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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Shop}/{action=Index}");

app.Run();