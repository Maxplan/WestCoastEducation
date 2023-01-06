using Microsoft.EntityFrameworkCore;
using WestCoastEdu.web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add database support...
builder.Services.AddDbContext<WestCoastEduContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"))
);

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed the database...
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<WestCoastEduContext>();
    await context.Database.MigrateAsync();
    await SeedData.LoadCourseData(context);
    await SeedData.LoadAccountData(context);
}
catch (Exception ex)
{
    Console.WriteLine("{0} - {1}", ex.Message, ex.InnerException!.Message);
    throw;
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
