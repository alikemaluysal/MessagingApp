using WebMVC;
using WebMVC.Hubs;
using WebMVC.Util.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(opt =>
{
    opt.Filters.Add<ExceptionAndToastFilter>();
}).AddNToastNotifyToastr();
builder.Services.AddMvcServices(builder.Configuration);


var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();    
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseNToastNotify();

app.MapHub<ChatHub>("/chatHub");

app.Run();
