using Microsoft.EntityFrameworkCore;
using Review.ApplicationCore.Contracts.Repositories;
using Review.ApplicationCore.Contracts.Services;
using Review.Infrastructure.Data;
using Review.Infrastructure.Repositories;
using Review.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.GetConnectionString("EShopDb");
builder.Services.AddDbContext<ReviewDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EShopDb")));

builder.Services.AddScoped<ICustomerReviewRepository, CustomerReviewRepository>();
builder.Services.AddScoped<ICustomerReviewService, CustomerReviewService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
