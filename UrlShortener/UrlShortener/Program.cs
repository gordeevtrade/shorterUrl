using Dal.Repositories;
using Domain.ServiceInterface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UrlShortener.BuisinessLogic;
using UrlShortener.Dal;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ShortenerContext>();

builder.Services.AddCors(options => options.AddPolicy(name: "UrlShortener",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateIssuerSigningKey = true,
              ValidIssuer = "https://localhost:7233/",
              ValidAudience = "https://localhost:7233/",
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123123123_�����23423423847�8��3489347�8�3948�7348�7������������34757465765������������������������"))
          };
      });

builder.Services.AddDbContext<ShortenerContext>(options =>
{
    options.UseSqlServer(connection);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<IUrlService, UrlService>();
builder.Services.AddScoped<IShortUrlRepository, UrlRepositories>();
builder.Services.AddScoped<UserManager<IdentityUser>, UserManager<IdentityUser>>();

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseRouting();
app.UseCors("UrlShortener");

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ShortenerContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();