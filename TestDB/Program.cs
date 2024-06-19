using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<TestDB.Db.ProjectDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Include;  // Include null values
    });

// Get the secret key and other configurations from appsettings.json
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();


var app = builder.Build();



// Configure the HTTP request pipeline.



app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Welcome to Clarity Implementation API!");
});


//app.MapGet("/favicon.ico", async context =>
//{
//    await context.Response.WriteAsync("Welcome to Clarity Implementation API!");
//});


app.MapControllers();

app.Run();
