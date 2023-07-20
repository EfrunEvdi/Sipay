using Microsoft.EntityFrameworkCore;
using Restful.WebApi.Configurations;
using Restful.WebApi.FakeService.Abstract;
using Restful.WebApi.FakeService.Concrete;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IUserService, UserManager>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddGlobalErrorHandler(); // app.AddGlobalErrorHandler() ifadesini kullanmak, uygulaman�n genel hata y�netimini kolayca yap�land�rmay� sa�lar. Bu sayede, hata i�leme mant��� tek bir yerde merkezi olarak tan�mlanabilir ve herhangi bir yerde meydana gelen hatalar tutarl� bir �ekilde ele al�nabilir.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
