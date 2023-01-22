using BookStore.Models.Domain;
using BookStore.Repository.Abstract;
using BookStore.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(option => {
        option.LoginPath = "/Access/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BSDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BSTconn")));

builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
     name: "default",
     pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
