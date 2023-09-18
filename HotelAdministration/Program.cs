using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HotelAdministration.Data;
using HotelAdministration.Areas.Identity.Data;


var builder = WebApplication.CreateBuilder(args); 
var connectionString = builder.Configuration.GetConnectionString("HotelAdministrationContextConnection") ?? throw new InvalidOperationException("Connection string 'HotelAdministrationContextConnection' not found.");

builder.Services.AddDbContext<HotelAdministrationContext>(options =>
    options.UseSqlServer(connectionString));  //connected with the db

builder.Services.AddDefaultIdentity<HotelAdministrationUser>()
    .AddDefaultTokenProviders() 
    .AddRoles<IdentityRole>() // in this way we will assign roles to the users
    .AddEntityFrameworkStores<HotelAdministrationContext>();

builder.Services.AddDbContext<TeamMembersDb>(options => options.UseSqlServer(connectionString)); // this is dependency injection


// Add services to the container.
builder.Services.AddControllersWithViews(); // if you create a controller, it will create a view using dependency injection
builder.Services.AddRazorPages(); // by default, it will support razor pages 

var app = builder.Build(); // this operation builds the app

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment()) // this is the development mode, it is very important to configure that
{
    app.UseExceptionHandler("/Home/Error"); // if we are not in the development mode, we will throw an error
}
app.UseStaticFiles(); // use static files in wwwroot

app.UseRouting();
app.UseAuthentication();;
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // if you don't give any account, it will head to index

using (var scope = app.Services.CreateScope()) // we are doing the seeding
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Employees" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope()) 
{
    var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<HotelAdministrationUser>>();

    string email = "admin@test.com";
    string password = "Test123.";

    if(await UserManager.FindByEmailAsync(email) == null)
    {
        var user = new HotelAdministrationUser();  // here I created the user 

        user.Firstname = email;
        user.LastName = email;
        user.UserName = email;
        user.Email = email;

        await UserManager.CreateAsync(user, password); // here I created him in the db

        await UserManager.AddToRoleAsync(user, "Admin");
    }
}

app.MapControllerRoute(
    name: "userManagement",
    pattern: "user",
    defaults: new { controller = "User", action = "Users" }
);


app.MapRazorPages();
app.Run(); // the application will be compiled and executed and will open in the browser
