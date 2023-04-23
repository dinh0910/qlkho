using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using qlkho.Data;
using AspNetCoreHero.ToastNotification;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<qlkhoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("qlkhoContext") ?? throw new InvalidOperationException("Connection string 'qlkhoContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options => {
    options.Cookie.Name = "QLKho";
    options.IdleTimeout = TimeSpan.FromDays(60);
});

builder.Services.AddNotyf(config => { config.DurationInSeconds = 4; config.IsDismissable = true; config.Position = NotyfPosition.TopCenter; });

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

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
