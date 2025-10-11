using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Caching;
using Core.DependencyResolvers;
using Core.Interfaces;
using Core.Utilities.Middlewares;
using Core.Utilities.Security.JWT;
using EasyShop.Api.Extensions;
using EasyShop.Api.Services.Providers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<UserForLoginValidator>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddBusinessServices();
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration, builder.Environment);


builder.Services.AddSwaggerWithJwt();


//Add Cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.WithOrigins("https://localhost:7118")
           .AllowAnyMethod()
           .AllowAnyHeader();
}));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("MyPolicy");

app.UseMiddleware<ExceptionMiddleware>(); //Global hata yönetimi

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

public partial class Program { }
