using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ThAmCoSystem;
using ThAmCoSystem.Areas.Identity.Data;
using ThAmCoSystem.IdentityDataSeeder;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEmailSender, EmailSender>();
;

// Register DbContext
builder.Services.AddDbContext<ThAmCoSystemContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ThAmCoSystemContextConnection"));
});

// Identity services
builder.Services.AddIdentity<ThAmCoSystemUser, IdentityRole>().AddDefaultTokenProviders()
   .AddEntityFrameworkStores<ThAmCoSystemContext>();


builder.Services.AddSignalR(); builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

 
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await IdentityDataSeeders.SeedAdminUserAndRole(serviceProvider);
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
app.UseAuthentication();;

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // Map route for areas
    endpoints.MapControllerRoute(
        name: "MyArea",
        pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

    // Map default route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // Map SignalR hub
});
app.MapRazorPages();

app.Run();
