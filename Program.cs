using MvcNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MvcNet.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
  // throw new Exception("Hai");
// Add services to the container.
builder.Services.AddControllersWithViews();

string host = builder.Configuration.GetValue<string>("MSSQL_HOST");
string user = builder.Configuration.GetValue<string>("MSSQL_USER");
string pass = builder.Configuration.GetValue<string>("MSSQL_PASS");
int port = builder.Configuration.GetValue<int>("MSSQL_PORT");
string db   = builder.Configuration.GetValue<string>("MSSQL_DB");
string hosts = builder.Configuration.GetValue<string>("AllowedHosts");
builder.Services.AddDbContext<AppDBContext>(
    opt => opt.UseSqlServer(@"Server="+host+","+port.ToString()
        +";Database="+db+";User Id="+user+";Password="+pass)
);

#region Identity Scafolding
/// Strange thing is the Scafold is strange? Can't use same DBContext
builder.Services.AddDbContext<MvcNetIdentityDbContext>(
    opt => opt.UseSqlServer(@"Server=" + host + "," + port.ToString()
        + ";Database=" + db + ";User Id=" + user + ";Password=" + pass)
);

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MvcNetIdentityDbContext>();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});
#endregion Identity Scafolding

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
