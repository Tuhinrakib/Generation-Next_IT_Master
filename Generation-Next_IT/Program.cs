using Generation_Next_IT.Data;
using Microsoft.EntityFrameworkCore;
using Generation_Next_IT.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
        
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying migrations or seeding the database.");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",

    pattern: "{controller=MeetingMinuteMasters}/{action=Create}/{id?}");
    
app.Run();
