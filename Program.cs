using Microsoft.EntityFrameworkCore;
using WebApplication11.DAL;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]));

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        // Add production error handling middleware here.
    }

    app.UseRouting(); // This should be added before UseEndpoints

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area}/{controller=Profession}/{action=index}/{id?}"
        );
    });
}


app.MapControllerRoute(
    name: "default", 
    pattern: "{controller=Home}/{action=index}/{ id?}"
    );
app.Run();
