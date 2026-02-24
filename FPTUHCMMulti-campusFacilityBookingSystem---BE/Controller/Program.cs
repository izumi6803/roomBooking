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

// IMPORTANT: Set the port BEFORE creating the builder
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
Console.WriteLine($"=== PORT CONFIGURATION ===");
Console.WriteLine($"PORT environment variable: {port}");
Console.WriteLine($"Setting ASPNETCORE_URLS to: http://0.0.0.0:{port}");
Environment.SetEnvironmentVariable("ASPNETCORE_URLS", $"http://0.0.0.0:{port}");

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext
// Support Railway DATABASE_URL (PostgreSQL) or appsettings.json DefaultConnection (SQL Server)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

// Debug logging
Console.WriteLine("=== DATABASE CONNECTION DEBUG ===");
Console.WriteLine($"DATABASE_URL from Environment: {(string.IsNullOrEmpty(databaseUrl) ? "NULL/EMPTY" : $"EXISTS (length: {databaseUrl.Length})")}");
Console.WriteLine($"DefaultConnection from Config: {(string.IsNullOrEmpty(connectionString) ? "NULL/EMPTY" : $"EXISTS (length: {connectionString.Length})")}");

// Also try getting from configuration (Railway might inject it differently)
var databaseUrlFromConfig = builder.Configuration["DATABASE_URL"];
Console.WriteLine($"DATABASE_URL from Configuration: {(string.IsNullOrEmpty(databaseUrlFromConfig) ? "NULL/EMPTY" : $"EXISTS (length: {databaseUrlFromConfig.Length})")}");

// Use whichever is available
databaseUrl = databaseUrl ?? databaseUrlFromConfig;
var usePostgres = !string.IsNullOrEmpty(databaseUrl);

Console.WriteLine($"Using PostgreSQL: {usePostgres}");

// Convert Railway DATABASE_URL format (postgresql://user:pass@host:port/db) to Npgsql format
string? finalConnectionString = null;
if (usePostgres && !string.IsNullOrEmpty(databaseUrl))
{
    try
    {
        // Parse the DATABASE_URL
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':');
        
        // Build Npgsql connection string
        finalConnectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
        
        Console.WriteLine("Successfully converted DATABASE_URL to Npgsql format");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error parsing DATABASE_URL: {ex.Message}");
        throw new InvalidOperationException("Failed to parse DATABASE_URL. Please check the format.", ex);
    }
}
else
{
    finalConnectionString = connectionString;
}

Console.WriteLine($"Final connection string: {(string.IsNullOrEmpty(finalConnectionString) ? "NULL/EMPTY" : "EXISTS")}");
Console.WriteLine("=== END DEBUG ===");

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

Console.WriteLine($"=== CORS CONFIGURATION ===");
Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");
Console.WriteLine($"Allowed Origins: {string.Join(", ", allowedOrigins)}");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        // TEMPORARY: Allow all origins for debugging
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
        
        Console.WriteLine("CORS: Allowing all origins (DEBUG MODE)");
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

// Apply database migrations automatically on startup (for Railway deployment)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FacilityBookingDbContext>();
    try
    {
        Console.WriteLine("Applying database migrations...");
        dbContext.Database.Migrate();
        Console.WriteLine("Database migrations applied successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
        throw;
    }
}

// Port is already configured at the top of the file before builder creation
// The app will automatically listen on the port set in ASPNETCORE_URLS

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
