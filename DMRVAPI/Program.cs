using DMRVAPI.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using DMRVAPI.Repositories.Service;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DMRVAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets("c879830e-46de-45b8-ae5a-4300030012bf");

// Add services to the container.
var serverVersion = new MariaDbServerVersion(new Version(10, 1, 48));
var connectionString = builder.Configuration["ConnectionStrings:MariaDbConnectionString"];
var steamApiKey = builder.Configuration["ConnectionStrings:SteamApiKey"];

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = SteamAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/user/get-token";
    })
    .AddSteam(options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.ApplicationKey = steamApiKey;

        options.Events.OnTicketReceived = context_ =>
        {
            var steamUserAsClaims = context_.Principal;
            var identityUser = context_.HttpContext.User;

            return Task.CompletedTask;

        };
        options.Events.OnAuthenticated = context_ =>
        {
            var steamUserAsClaims = context_.Identity;
            var nameIdentifier = steamUserAsClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            // var name = steamUserAsClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            // NOPE: Identity user not initialized yet in context_.HttpContext.User

            context_.HttpContext.User.Claims.Append(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
            // context_.HttpContext.User.Claims.Append(new Claim(ClaimTypes.Name, name));

            return Task.CompletedTask;
        };
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["ConnectionStrings:Jwt:Issuer"],
            ValidAudience = builder.Configuration["ConnectionStrings:Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ConnectionStrings:Jwt:SecretKey"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
    config.AddPolicy(Policies.Moderator, Policies.ModeratorPolicy());
    config.AddPolicy(Policies.User, Policies.UserPolicy());
});

builder.Services.Configure<CookiePolicyOptions>(options =>
    {
        options.MinimumSameSitePolicy = SameSiteMode.None;
        options.Secure = CookieSecurePolicy.Always;
        options.OnDeleteCookie = context => context.CookieOptions.SameSite = SameSiteMode.None;
        options.OnAppendCookie = context => context.CookieOptions.SameSite = SameSiteMode.None;
    });

builder.Services.AddCors(o => o.AddPolicy("WebAPI", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    }));
builder.Services.AddDbContext<MariaDbContext>(
        options => options
            .UseMySql(connectionString,
                      new MariaDbServerVersion(new Version(10, 1, 48)))

            // The following three options help with debugging, but should
            // be changed or removed for production.
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
    );
builder.Services.AddEntityFrameworkMySql();
builder.Services.AddScoped<IMariaDbUserService, MariaDbUserService>();
builder.Services.AddScoped<IMariaDbSongService, MariaDbSongService>();
builder.Services.AddScoped<IMariaDbPatternService, MariaDbPatternService>();
builder.Services.AddScoped<IMariaDbRecordService, MariaDbRecordService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseHttpsRedirection();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();
