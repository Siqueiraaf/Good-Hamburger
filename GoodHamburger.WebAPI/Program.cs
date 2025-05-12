using FluentValidation;
using FluentValidation.AspNetCore;
using GoodHamburger.Domain.Services;
using GoodHamburger.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<OrderDtoValidator>();

builder.Services.AddSingleton<OrderService>();
builder.Services.AddSingleton<InMemoryOrderRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
