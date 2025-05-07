using dotnet9_ketnoigiaothuong.Domain.Validators;
using dotnet9_ketnoigiaothuong.Infrastructure.Context;
using dotnet9_ketnoigiaothuong.Infrastructure.Exceptions;
using dotnet9_ketnoigiaothuong.Infrastructure.Mapping;
using dotnet9_ketnoigiaothuong.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.AuthContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.QuotationRequestContract;
using static dotnet9_ketnoigiaothuong.Domain.Contracts.QuotationResponseContract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#region Register services in Dependency Injection

builder.Services.AddScoped<IValidator<LoginViewModel>, LoginViewModelValidator>();
builder.Services.AddScoped<IValidator<RegisterViewModel>, RegisterViewModelValidator>();
builder.Services.AddScoped<DbInitializer>();
builder.Services.AddScoped<IValidator<CreateQuotationRequest>, CreateQuotationRequestValidator>();
builder.Services.AddScoped<IValidator<CreateQuotationResponse>, CreateQuotationResponseValidator>();
builder.Services.AddScoped<IValidator<UpdateQuotationResponse>, UpdateQuotationResponseValidator>();

#endregion

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwt = builder.Configuration.GetSection("Jwt");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Cookies["jwt"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
})
.AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/api/auth/login";
});

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Kết Nối Giao Thương API",
        Version = "v1",
        Description = "API for connecting businesses and trade partners"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();

app.MapControllers();

// Initialize data
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    dbInitializer.SeedData();
}

app.Run();
