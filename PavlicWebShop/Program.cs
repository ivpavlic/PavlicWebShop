using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PavlicWebShop.Data;
using PavlicWebShop.Models.Dbo;
using PavlicWebShop.Models.Dto;
using PavlicWebShop.Services.Implementation;
using PavlicWebShop.Services.Interface;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddControllersWithViews();

builder.Services.Configure<AppConfig>(builder.Configuration);
builder.Services.AddSingleton<IIdentityService, IdentityService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IFileStorageService, FileStorageService>();
//builder.Services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, QueueProcessor>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.Services.GetService<IIdentityService>();

app.Run();
