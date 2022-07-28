using Mango.Web;
using Mango.Web.Services;
using Mango.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IProductService, ProductService>();

SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
.AddOpenIdConnect("oidc", options=>
{
    options.Authority= builder.Configuration["ServiceUrls:IdentityServer"];
    options.GetClaimsFromUserInfoEndpoint = true;
    options.ClientId = "mango";
    options.ClientSecret = "secret";
    options.ResponseType = "code";
    options.TokenValidationParameters.NameClaimType = "name";
    options.TokenValidationParameters.RoleClaimType = "role";
    options.Scope.Add("mango");
    options.SaveTokens = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

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

app.Run();
