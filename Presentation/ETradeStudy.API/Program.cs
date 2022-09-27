using ETradeStudy.Application;
using ETradeStudy.Application.Validatiors.Product;
using ETradeStudy.Infrastructure;
using ETradeStudy.Infrastructure.Enums;
using ETradeStudy.Infrastructure.Filters;
using ETradeStudy.Infrastructure.Services.Storage.Local;
using ETradeStudy.Percistance;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("https://localhost:4200", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddPersistanceServices();
builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage(StorageType.LocalStorage);
// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()).
    AddFluentValidation(options => options.
    RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).
    ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin",options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
