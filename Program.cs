using ChTestPro.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

var ConnectionString = configuration.GetConnectionString("DefaultConnection");

//Connection DataBase
builder.Services.AddDbContext<ChTestDbContext>(options =>
            options.UseSqlServer(ConnectionString));

//Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "ChTestProCookie";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        options.SlidingExpiration = true;
        options.LoginPath = "/Home/Index";
        options.LogoutPath = "/Home/LogOut";
        options.Events = new CookieAuthenticationEvents
        {
            OnValidatePrincipal = context =>
            {
                // Verifica si la cookie ha expirado
                if (context.Properties.ExpiresUtc.HasValue && context.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow)
                {
                    context.RejectPrincipal(); // Rechaza la cookie expirada
                    context.Response.Redirect("/Home/LogOut"); // Redirige al Index del controlador Home
                }
                return Task.CompletedTask;
            }
        };
    });

//Razor Pages
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
