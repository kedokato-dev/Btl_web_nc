using Btl_web_nc.Data;
using Btl_web_nc.Models;
using Btl_web_nc.Repositories;
using Btl_web_nc.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<IRegisterRepository, RegisterRepository>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<RegisterService>();
builder.Services.AddScoped<ITopicRepository, TopicRepository>();
builder.Services.AddScoped<TopicServices>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<ProfileService>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ArticleRepository>();
builder.Services.AddScoped<RssService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<SendMailServices>();
builder.Services.AddScoped<EmailNotificationService>();

builder.Services.AddScoped<INewsletterRegisterRepository, NewsletterRegisterRepository>();
builder.Services.AddScoped<NewsletterRegisterServices>();

builder.Services.AddScoped<INewsletterRepository, NewsletterRepository>();
builder.Services.AddScoped<NewsletterServices>();

builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ArticlesServices>();

builder.Services.AddSingleton<EmailBackgroundService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<EmailBackgroundService>());

builder.Services.AddHostedService<EmailBackgroundService>();

// service rss feed
// Thêm vào Program.cs trong phần ConfigureServices
builder.Services.AddScoped<RssService>();

// service scraper data
builder.Services.AddScoped<NewsScraperService>();


// Cấu hình MySQL cho EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Login/AccessDenied";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
     options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
    options.AddPolicy("BannedOnly", policy => policy.RequireRole("Banned"));
});

builder.Services.AddControllersWithViews()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();
               


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
