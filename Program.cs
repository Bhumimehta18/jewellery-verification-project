using Microsoft.EntityFrameworkCore;
using JewelleryVerificationProject.Data;  // your DbContext namespace


var builder = WebApplication.CreateBuilder(args);

// ✅ Add MVC Controllers with Views
builder.Services.AddControllersWithViews();

// ✅ Configure EF Core to use SQLite (with absolute path from appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AppDbContext")));

// ✅ Add Session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// ✅ Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // Enable session
app.UseAuthorization();

// ✅ Analytics page route
app.MapControllerRoute(
    name: "analytics",
    pattern: "Analytics/{action=Index}/{id?}",
    defaults: new { controller = "Analytics" });

// ✅ Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Welcome}/{id?}");
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // ✅ ensures tables like "Jewellery" are created
}

app.Run();
