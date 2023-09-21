using Microsoft.EntityFrameworkCore;
using OuTouchFilms.Models;
using OuTouchFilms.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<IFilmService, FilmService>();

builder.Services.AddDbContext<OuTouchDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("TestOuTouchDb"), confOptions =>
    {
        confOptions.SetPostgresVersion(new Version("9.6"));
    });
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Films}/{action=Index}/{id?}");

app.Run();
