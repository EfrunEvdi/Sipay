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

app.AddGlobalErrorHandler(); // app.AddGlobalErrorHandler() ifadesini kullanmak, uygulamanýn genel hata yönetimini kolayca yapýlandýrmayý saðlar. Bu sayede, hata iþleme mantýðý tek bir yerde merkezi olarak tanýmlanabilir ve herhangi bir yerde meydana gelen hatalar tutarlý bir þekilde ele alýnabilir.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
