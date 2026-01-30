using DAL.Dbcontext;
using DAL.Repositories;
using BLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Controller.Converters;
using Controller.Filters;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext
// Support Railway DATABASE_URL (PostgreSQL) or appsettings.json DefaultConnection (SQL Server)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
var usePostgres = !string.IsNullOrEmpty(databaseUrl);

// Railway provides DATABASE_URL for PostgreSQL, use it if available; otherwise use SQL Server
var finalConnectionString = usePostgres ? databaseUrl : connectionString;

if (string.IsNullOrEmpty(finalConnectionString))
{
    throw new InvalidOperationException("No database connection string found. Please set DefaultConnection in appsettings.json or DATABASE_URL environment variable.");
}

builder.Services.AddDbContext<FacilityBookingDbContext>(options =>
{
    if (usePostgres)
    {
        options.UseNpgsql(finalConnectionString);
    }
    else
    {
        options.UseSqlServer(finalConnectionString);
    }
});

// Register Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register all services using ServiceProviders
ServiceProviders.RegisterServices(builder.Services);

// Configure JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? string.Empty;
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? string.Empty;
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? string.Empty;

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
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero
    };
});

// Configure CORS
// In production, update WithOrigins to include your Vercel frontend URL
// Example: policy.WithOrigins("https://your-project.vercel.app", "http://localhost:5173")
var allowedOrigins = builder.Configuration["AllowedOrigins"]?.Split(';') 
    ?? new[] { "http://localhost:5173", "http://localhost:3000" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            // Development: Allow all origins
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        }
        else
        {
            // Production: Only allow specific origins
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        }
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        // Format DateTime theo định dạng "dd/MM/yyyy HH:mm:ss"
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
        options.JsonSerializerOptions.Converters.Add(new NullableDateTimeConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FPTU Facility Booking API",
        Version = "v1",
        Description = "API cho Hệ thống Đặt Cơ Sở Vật Chất FPTU"
    });

    // Enable XML comments
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Add enum descriptions to show in dropdown
    c.UseInlineDefinitionsForEnums();

    // Map IFormFile để Swagger biết cách xử lý
    c.MapType<IFormFile>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "binary"
    });
    
    c.MapType<IFormFile[]>(() => new OpenApiSchema
    {
        Type = "array",
        Items = new OpenApiSchema
        {
            Type = "string",
            Format = "binary"
        }
    });
    
    c.MapType<List<IFormFile>>(() => new OpenApiSchema
    {
        Type = "array",
        Items = new OpenApiSchema
        {
            Type = "string",
            Format = "binary"
        }
    });

    // Add support for file uploads in Swagger
    c.OperationFilter<FileUploadOperationFilter>();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập JWT token của bạn"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(Applications.Mappers.MappingProfile));

var app = builder.Build();

// Configure port for Railway/production deployment
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    app.Urls.Add($"http://0.0.0.0:{port}");
}

// Configure the HTTP request pipeline.
// Enable Swagger in production for API documentation
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FPTU Facility Booking API v1");
    c.RoutePrefix = "swagger";
});

// Only use HTTPS redirection in production (Railway handles HTTPS)
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
