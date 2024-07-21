using Microsoft.EntityFrameworkCore;
using WebApplication11.DAL;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area}/{controller=Profession}/{action=index}/{id?}"
    );
});


app.MapControllerRoute(
    name: "default", 
    pattern: "{controller=Home}/{action=index}/{ id?}"
    );
app.Run();
