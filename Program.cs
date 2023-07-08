 using Hamro_Pasal.Data;
using Hamro_Pasal.Interfaces;
using Hamro_Pasal.Models;
using Hamro_Pasal.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();











//adding connection to database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Hamro_Pasal.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));




//adding identity services

builder.Services.AddIdentity<Authenticate,IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    //options.Password.RequireNonAlphanumeric = false;
    //options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();



//adding token gererator
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "http://ahmadmozaffar.net",
        ValidIssuer = "http://ahmadmozaffar.net",
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is the key that we will use in the encryption")),
        ValidateIssuerSigningKey = true,
    };
});


builder.Services.AddScoped<IUserInterface, AuthenticateUserRepository>();

builder.Services.AddScoped<IAdsSelectInterface, AdsSelectRepository>();
builder.Services.AddScoped<IHomeInterface, HomeRepository>();

builder.Services.AddScoped<IPostAdsInterface, PostAdsRepository>();

builder.Services.AddScoped<IUserDetailsInterface, UserDetailsRepository>();



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




builder.Services.AddRazorPages();





var app = builder.Build();




if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();



    app.MapControllerRoute(
        name: "my_route",
        pattern: "{controller=Register}/{action=Index}/{id?}");
    
    app.MapRazorPages();

    app.Run();