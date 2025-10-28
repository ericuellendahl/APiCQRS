using ApiCQRS.Api.Configurations;
using ApiCQRS.Aplication.DTOs;
using ApiCQRS.Aplication.UseCase.Order.Commands;
using ApiCQRS.Aplication.UseCase.Order.Queries;
using ApiCQRS.Domian.Interfaces.Orders;
using ApiCQRS.Infra.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseCQRS")));

builder.Services.AddScoped<IQueryHandler<GetOrderByIdQuery, OrderDto>, GetOrderQueryHandler>();
builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, OrderDto>, CreateOrderCommandHandler>();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddUseCases();

var app = builder.Build();

app.MapDefaultEndpoints();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
