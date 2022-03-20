using MvcNet;
using Microsoft.EntityFrameworkCore;

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
