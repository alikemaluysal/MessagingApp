using Application;
using Microsoft.AspNetCore.Authentication.Cookies;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.Cookie.Name = "auth-cookie";
               options.Cookie.HttpOnly = true;
               options.Cookie.IsEssential = true;
               options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

               options.LoginPath = "/Login";
               options.LogoutPath = "/Logout";
               options.AccessDeniedPath = "/AccessDenied";
               options.ReturnUrlParameter = "returnUrl";
               options.SlidingExpiration = true;
           });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
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

app.Run();
