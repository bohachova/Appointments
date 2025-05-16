using Appointments.BL.Abstracts;
using Appointments.BL.Profiles;
using Appointments.BL.Queries;
using Appointments.BL.Security;
using Appointments.BL.Services;
using Appointments.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetServiceListQuery).Assembly));
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = JWTAuthOptions.ISSUER,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = JWTAuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IHasher, Hasher>();
builder.Services.AddScoped<IRefreshTokenManager, RefreshTokenManager>();  
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppointmentsDbContext(builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(ServiceProfile)));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
