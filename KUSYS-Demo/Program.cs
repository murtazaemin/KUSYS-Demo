using KUSYS_Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<DataSeeder>();

builder.Services.AddDbContext<Context>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext"), providerOptions =>
                   providerOptions.EnableRetryOnFailure(3)
               )
           );
// Cookie ayarlarý
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(options => 
    {
        options.Cookie.Name = "MyCookie"; 
        options.LoginPath = "/Login/Login";
    });
var app = builder.Build();

var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
// dataseeder classýndan verilerin eklenmesi
using (var scope = scopedFactory.CreateScope())
{
    var service = scope.ServiceProvider.GetService<DataSeeder>();
    service.Seed();
}

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
    pattern: "{controller=StudentsCourse}/{action=Index}/{id?}");

app.Run();
