using Core.Configuration;
using Core.Interfaces;
using Infrastructure.Repositories;
using Logic.Handlers;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();
builder.Services.AddTransient<IGetAllOrdersHandler, GetAllOrdersHandler>();
builder.Services.AddTransient<IGetAllOrdersByUsernameHandler, GetAllOrdersByUsernameHandler>();
builder.Services.AddTransient<IGetAllOrdersByTypeHandler, GetAllOrdersByTypeHandler>();
builder.Services.AddTransient<ICreateOrderHandler, CreateOrderHandler>();
builder.Services.AddTransient<IUpdateOrderHandler, UpdateOrderHandler>();
builder.Services.AddTransient<IDeleteOrderHandler, DeleteOrderHandler>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
