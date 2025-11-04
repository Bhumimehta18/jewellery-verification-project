using Microsoft.EntityFrameworkCore;
using JewelleryVerificationProject.Data;  // ✅ your DbContext namespace

var builder = WebApplication.CreateBuilder(args);

// ✅ Add MVC Controllers with Views
builder.Services.AddControllersWithViews();

// ✅ Configure EF Core with SQLite
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

// ✅ Error handling for Production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // 🔹 Optional but recommended for HTTPS
}

app.UseHttpsRedirection(); // 🔹 Add this (Render supports HTTPS)
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

// ✅ Analytics route
app.MapControllerRoute(
    name: "analytics",
    pattern: "Analytics/{action=Index}/{id?}",
    defaults: new { controller = "Analytics" });

// ✅ Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Welcome}/{id?}");

// ✅ Ensure database is created and migrated
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Database migration failed: {ex.Message}");
    }
}


app.Run();
