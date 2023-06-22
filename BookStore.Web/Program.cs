using BookStore.Web.DBContext;
using BookStore.Web.Models;
using BookStore.Web.Repositories;
using BookStore.Web.Services;
using BookStore.Web.Settings;
using BookStore.Web.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IValidator<BookModel>, BookModelValidator>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddSingleton<MongoContext>();
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoConnection"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
