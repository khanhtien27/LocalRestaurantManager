
using ManagementRestaurentWeb;
using ManagementRestaurentWeb.Service;
using ManagementRestaurentWeb.Service.IService;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapperCofi));
builder.Services.AddHttpClient<IRestaurentService, RestaurentService>();
builder.Services.AddScoped<IRestaurentService, RestaurentService>();

builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddHttpClient<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//take token and save it everytime call api
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.Cookie.HttpOnly = true;
        option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        option.LoginPath = "/Auth/Login";
        option.AccessDeniedPath = "/Auth/AccessDenied";
        option.SlidingExpiration = true;
    });

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(100);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseSession(); //take token, configue 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
