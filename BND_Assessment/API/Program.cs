using API.Database;
using API.Database.Repositories;
using API.Infrastructure;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("AssessmentDB"));

builder.Services.AddTransient<ICustomerAccountRepository, CustomerAccountRepository>();
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();

builder.Services.AddScoped<ICustomerAccountService, CustomerAccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(ErrorHandlingMiddleware));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
